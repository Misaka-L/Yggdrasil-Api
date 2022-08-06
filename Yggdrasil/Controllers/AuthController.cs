using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yggdrasil.Filters;
using Yggdrasil.Models.YggdrasilApi;
using Yggdrasil.Service;

namespace Yggdrasil.Controllers {
    /// <summary>
    /// authserver
    /// </summary>
    [Route("authserver")]
    [ApiController]
    [AuthorizationFilter]
    public class AuthController : ControllerBase {
        private readonly YggdrasilService _yggdrasilService;

        public AuthController(YggdrasilService yggdrasilService) {
            _yggdrasilService = yggdrasilService;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="request">验证请求</param>
        /// <returns><see cref="AuthenticateResponse"/></returns>
        [Route("authenticate")]
        [HttpPost]
        [AllowAnonymous]
        public async ValueTask<AuthenticateResponse> Authenticate(AuthenticateRequest request) {
            Guid clientToken = Guid.NewGuid();
            if (!Guid.TryParse(request.ClientToken, out clientToken)) clientToken = Guid.NewGuid();
            return await _yggdrasilService.AuthenticateAsync(request.Username, request.Password, clientToken);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns><see cref="AuthenticateResponse"/></returns>
        [Route("refresh")]
        [HttpPost]
        public async ValueTask<AuthenticateResponse> Refresh(RefreshRequest request) {
            Guid clientToken = Guid.NewGuid();
            if (!Guid.TryParse(request.ClientToken, out clientToken)) clientToken = Guid.NewGuid();
            return await _yggdrasilService.RefreshAsync(Guid.Parse(HttpContext.User.Identity.Name), request.SelectedProfile.Id, clientToken);
        }

        /// <summary>
        /// 验证是否有效
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>HTTP 402 No Content</returns>
        [Route("validate")]
        [HttpPost]
        public IActionResult Validate(TokenRequestResponse request) {
            return NoContent();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>HTTP 402 No Content</returns>
        [Route("signout")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Signout(LoginRequest request) {
            return NoContent();
        }

        /// <summary>
        /// 吊销 Token
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>HTTP 402 No Content</returns>
        [Route("invalidate")]
        [HttpPost]
        public IActionResult Invalidate(TokenRequestResponse request) {
            return NoContent();
        }
    }
}
