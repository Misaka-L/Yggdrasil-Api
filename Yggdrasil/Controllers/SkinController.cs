using Microsoft.AspNetCore.Mvc;
using Yggdrasil.Models.Yggdrasil;
using Yggdrasil.Models.YggdrasilApi;
using Yggdrasil.Service;

namespace Yggdrasil.Controllers {
    /// <summary>
    /// SkinApi
    /// </summary>
    [Route("api")]
    [ApiController]
    public class SkinController : ControllerBase {
        private readonly YggdrasilService _yggdrasilService;

        public SkinController(YggdrasilService yggdrasilService) {
            _yggdrasilService = yggdrasilService;
        }

        /// <summary>
        /// 获取多个角色
        /// </summary>
        /// <param name="profileNames">角色名称列表</param>
        /// <returns></returns>
        [Route("profiles/minecraft")]
        [HttpPost]
        public async ValueTask<YggdrasilProfile[]> GetProfiles(string[] profileNames) {
            return await _yggdrasilService.GetProfilesByNames(profileNames);
        }

        /// <summary>
        /// 上传皮肤
        /// </summary>
        /// <param name="uuid">角色 UUID</param>
        /// <param name="textureType">材质类型</param>
        /// <returns>HTTP 204 NoContet</returns>
        [Route("user/profile/{uuid}/{textureType}")]
        [HttpPut]
        public IActionResult UploadSkin(Guid uuid, string textureType, [FromBody] UploadSkinFromData model) {
            return Ok();
        }

        /// <summary>
        /// 删除皮肤
        /// </summary>
        /// <param name="uuid">角色 UUID</param>
        /// <param name="textureType">材质类型</param>
        /// <returns>HTTP 204 NoContet</returns>
        [Route("user/profile/{uuid}/{textureType}")]
        [HttpDelete]
        public IActionResult DeleteSkin(Guid uuid, string textureType) {
            return Ok();
        }
    }
}
