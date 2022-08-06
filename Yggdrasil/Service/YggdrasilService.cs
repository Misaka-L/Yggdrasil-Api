using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Yggdrasil.Models.Options;
using Yggdrasil.Models.Yggdrasil;
using Yggdrasil.Models.YggdrasilApi;

namespace Yggdrasil.Service {
    public class YggdrasilService {
        private readonly IYggdrasilConnecterService _yggdrasilConnecterService;
        private readonly IOptions<JwtOption> _jwtOption;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<YggdrasilService> _logger;
        public YggdrasilService(IYggdrasilConnecterService yggdrasilConnecterService, IOptions<JwtOption> jwtOption, IDistributedCache distributedCache, ILogger<YggdrasilService> logger) {
            _yggdrasilConnecterService = yggdrasilConnecterService;
            _jwtOption = jwtOption;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async ValueTask<AuthenticateResponse> AuthenticateAsync(string username, string password, Guid? clientToken = null) {
            var user = await _yggdrasilConnecterService.AuthenticateAsync(username, password);
            var availableProfiles = await _yggdrasilConnecterService.GetUserProfiles(user.Id);
            var selectedProfile = await _yggdrasilConnecterService.GetUserSelectedProfile(user.Id);

            var accessToken = getToken(user, selectedProfile);

            var response = new AuthenticateResponse {
                User = user,
                AvailableProfiles = availableProfiles,
                SelectedProfile = selectedProfile,
                AccessToken = accessToken
            };

            if (clientToken.HasValue) {
                response.ClientToken = clientToken.GetValueOrDefault().ToString("N");
            } else {
                response.ClientToken = Guid.NewGuid().ToString("N");
            }

            _logger.LogInformation("{0}({1}) 验证成功", username, user.Id);
            return response;
        }

        public async ValueTask<AuthenticateResponse> RefreshAsync(Guid uuid, Guid? selectProfile, Guid? clientToken = null) {
            var user = await _yggdrasilConnecterService.GetUser(uuid);
            var availableProfiles = await _yggdrasilConnecterService.GetUserProfiles(uuid);

            YggdrasilProfile selectedProfile = selectProfile is Guid selectProfileId
                ? await _yggdrasilConnecterService.SetUserSelectedProfile(uuid, selectProfileId)
                : await _yggdrasilConnecterService.GetUserSelectedProfile(uuid);

            var accessToken = getToken(user, selectedProfile);

            var response = new AuthenticateResponse {
                User = user,
                AvailableProfiles = availableProfiles,
                SelectedProfile = selectedProfile,
                AccessToken = accessToken
            };

            if (clientToken.HasValue) {
                response.ClientToken = clientToken.GetValueOrDefault().ToString("N");
            } else {
                response.ClientToken = Guid.NewGuid().ToString("N");
            }

            _logger.LogInformation("{1} 刷新令牌成功，选择角色 {0}({1})", user.Id, selectedProfile.Name, selectedProfile.Id);
            return response;
        }

        public async Task JoinServer(Guid userId, Guid profileId, string serverId, string ip) {
            if (await _yggdrasilConnecterService.GetProfile(profileId) is YggdrasilProfile profile) {
                await _distributedCache.SetStringAsync(profile.Name, JsonSerializer.Serialize(new YggdrasilJoinServerInfo {
                    ServerId = serverId,
                    ProfileId = profile.Id,
                    Ip = ip
                }), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30)));

                _logger.LogInformation("{0} 发起进入服务器 {1} 请求，角色:{2}({3})，IP:[{4}]", userId, serverId, profile.Name, profile.Id, ip);
            }
        }

        public async ValueTask<YggdrasilProfile?> JoinedServer(string userName, string serverId, string? ip = null) {
            if (await _distributedCache.GetStringAsync(userName) is string cachedString &&
                JsonSerializer.Deserialize<YggdrasilJoinServerInfo>(cachedString) is YggdrasilJoinServerInfo info) {
                await _distributedCache.RemoveAsync(userName);
                if (!string.IsNullOrEmpty(ip) && ip != info.Ip) {
                    _logger.LogError("服务器 {0} 验证玩家 {1} 加入服务器请求失败: IP 不匹配，IP:[{2}]", serverId, userName, ip);
                    return null;
                }

                if (await _yggdrasilConnecterService.GetProfile(info.ProfileId) is YggdrasilProfile profile) {
                    _logger.LogInformation("服务器 {0} 成功验证角色 {1}({2}) 加入服务器请求，IP:[{3}]", serverId, profile.Name, profile.Id, ip);

                    return profile;
                }
            }

            _logger.LogError("服务器 {0} 验证玩家 {1} 加入服务器请求失败，IP:[{2}]", serverId, userName, ip);
            return null;
        }

        public ClaimsPrincipal GetTokenInfo(string token) {
            var tokenValidationParameters = new TokenValidationParameters {
                ValidIssuer = "Yggdrasil Api",
                ValidAudience = "Yggdrasil Api",
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.Secret)),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var claimsPrincipal =
                jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            var validatedSecurityAlgorithm = validatedToken is JwtSecurityToken jwtSecurityToken
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

            if (validatedSecurityAlgorithm) {
                return claimsPrincipal;
            } else {
                throw new Exception("fail");
            }
        }

        public async ValueTask<YggdrasilProfile?> GetProfile(Guid uuid, bool unsigned = true) {
            return await _yggdrasilConnecterService.GetProfile(uuid);
        }

        public async ValueTask<YggdrasilProfile[]> GetProfilesByNames(string[] userNames) {
            return await _yggdrasilConnecterService.GetProfilesByNames(userNames);
        }

        private string getToken(YggdrasilUser user, YggdrasilProfile profile) {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim("pro", profile.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.Secret));
            var token = new JwtSecurityToken(
                issuer: "Yggdrasil Api",
                audience: "Yggdrasil Api",
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtOption.Value.ExpiresHour),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}