using Microsoft.AspNetCore.Mvc;
using Yggdrasil.Filters;
using Yggdrasil.Models.Yggdrasil;
using Yggdrasil.Models.YggdrasilApi;
using Yggdrasil.Service;

namespace Yggdrasil.Controllers {
    /// <summary>
    /// 会话
    /// </summary>
    [Route("sessionserver/session/minecraft")]
    [ApiController]
    public class SessionController : ControllerBase {
        private readonly YggdrasilService _yggdrasilService;

        public SessionController(YggdrasilService yggdrasilService) {
            _yggdrasilService = yggdrasilService;
        }

        /// <summary>
        /// 加入服务器
        /// </summary>
        /// <param name="request">加入服务器请求</param>
        /// <returns>HTTP 204 No Content</returns>
        [Route("join")]
        [HttpPost]
        [AuthorizationFilter]
        public async ValueTask<IActionResult> Join(JoinServerRequest request) {
            var selectedProfileId = Guid.Parse(request.SelectedProfile);
            if (Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "pro").Value) != selectedProfileId) {
                var response = new JsonResult(new ExceptionResponse {
                    Error = "ForbiddenOperationException",
                    ErrorMessage = "Invalid token."
                });

                response.StatusCode = StatusCodes.Status403Forbidden;
                return response;
            }

            await _yggdrasilService.JoinServer(selectedProfileId,
                Guid.Parse(request.SelectedProfile),
                request.ServerId,
                HttpContext.Connection.RemoteIpAddress.ToString());

            return NoContent();
        }

        /// <summary>
        /// 是否加入服务器
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="serverId">服务器 Id</param>
        /// <param name="ip">Ip</param>
        /// <returns></returns>
        [Route("hasJoined")]
        [HttpGet]
        public async ValueTask<YggdrasilProfile?> HasJoined(string username, string serverId, string? ip) {
            return await _yggdrasilService.JoinedServer(username, serverId, ip);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="uuid">角色 uuid</param>
        /// <param name="unsigned">是否签名</param>
        /// <returns></returns>
        [Route("profile/{uuid}")]
        [HttpGet]
        public async ValueTask<YggdrasilProfile?> Profile(Guid uuid, bool unsigned = true) {
            return await _yggdrasilService.GetProfile(uuid, unsigned);
        }
    }
}
