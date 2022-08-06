using System.Net.Http.Headers;
using Yggdrasil.Models.Connecter;
using Yggdrasil.Models.Yggdrasil;

namespace Yggdrasil.Service {
    public class YggdrasilConnecterService : IYggdrasilConnecterService {
        private readonly HttpClient _client;

        public YggdrasilConnecterService(HttpClient client) {
            _client = client;
        }

        public async ValueTask<YggdrasilUser> AuthenticateAsync(string username, string password) {
            var response = await _client.PostAsJsonAsync("/api/login", new {
                account = username,
                password = password
            });

            if (await response.Content.ReadFromJsonAsync<ApiResponse<string>>() is ApiResponse<string> result) {
                if (!result.success) throw new Exception("login fail");

                var tempClient = new HttpClient {
                    BaseAddress = new Uri("http://localhost:5566/")
                };

                tempClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.data);
                tempClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("YggdrasilApi", "0.0.1"));
                if (await tempClient.GetFromJsonAsync<ApiResponse<ApiUser>>("/api/getLoginUser") is ApiResponse<ApiUser> info) {
                    return new YggdrasilUser {
                        Id = info.data.uuid
                    };
                }
            }

            throw new Exception("login fail");
        }

        public async ValueTask<YggdrasilProfile?> GetProfile(Guid uuid) {
            return (await _client.GetFromJsonAsync<ApiResponse<ApiProfile>>($"/api/yggdrasilprofile/uuid?uuid={uuid}")).data.ToYggdrasilProfile();
        }

        public ValueTask<YggdrasilProfile[]> GetProfilesByNames(string[] userNames) {
            throw new NotImplementedException();
        }

        public async ValueTask<YggdrasilUser> GetUser(Guid uuid) {
            var user = await _client.GetFromJsonAsync<ApiResponse<ApiUser>>($"/api/yggdrasilprofile/user?uuid={uuid}");

            return new YggdrasilUser {
                Id = user.data.uuid
            };
        }

        public async ValueTask<YggdrasilProfile[]> GetUserProfiles(Guid uuid) {
            var result = (await _client.GetFromJsonAsync<ApiResponse<ApiProfile[]>>($"/api/yggdrasilprofile/userProfiles?userId={uuid}")).data;
            var profiles = new List<YggdrasilProfile>();
            foreach (var item in result) {
                profiles.Add(item.ToYggdrasilProfile());
            }

            return profiles.ToArray();
        }

        public async ValueTask<YggdrasilProfile> GetUserSelectedProfile(Guid uuid) {
            return (await _client.GetFromJsonAsync<ApiResponse<ApiProfile>>($"/api/yggdrasilprofile/userProfile?userId={uuid}")).data.ToYggdrasilProfile();
        }

        public async ValueTask<YggdrasilProfile> SetUserSelectedProfile(Guid userId, Guid uuid) {
            var response = await _client.PostAsync($"/api/yggdrasilprofile/setSelectedProfile?userId={userId}&uuid={uuid}", null);

            return (await response.Content.ReadFromJsonAsync<ApiResponse<ApiProfile>>()).data.ToYggdrasilProfile();
        }
    }
}
