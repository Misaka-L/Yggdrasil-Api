using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Yggdrasil.Helper;
using Yggdrasil.Models.Options;
using Yggdrasil.Models.YggdrasilApi;

namespace Yggdrasil.Controllers {
    /// <summary>
    /// 获取服务端元数据
    /// </summary>
    [Route("/")]
    [ApiController]
    public class MainYggdrasilController : ControllerBase {
        private readonly YggdrasilOption _yggdrasilOption;
        public MainYggdrasilController(IOptions<YggdrasilOption> yggdrasilOption) {
            _yggdrasilOption = yggdrasilOption.Value;
        }

        [HttpGet]
        public YggdrasilMetaDataResponse Index() {
            return new YggdrasilMetaDataResponse {
                Meta = new YggdrasilMetaData {
                    ServerName = _yggdrasilOption.ServerName,
                    ImplementationName = "Api Yggdrasil Interface",
                    ImplementationVersion = "0.0.1",

                    IsSupportNoEmailLogin = _yggdrasilOption.IsSupportNoEmailLogin,
                    IsSupportLegacySkinApi = _yggdrasilOption.IsSupportLegacySkinApi,
                    IsMojangNamespaceDisabled = _yggdrasilOption.IsMojangNamespaceDisabled,
                    IsUsernameCheckEnabled = _yggdrasilOption.IsUsernameCheckEnabled,
                    IsMojangAntiEnabled = _yggdrasilOption.IsMojangAntiEnabled,

                    Links = new YggdrasilLink {
                        Homepage = _yggdrasilOption.HomepageLink,
                        Register = _yggdrasilOption.RegisterLink
                    }
                },
                SkinDomains = _yggdrasilOption.SkinDomains,
                SignaturePublickey = RSAHelper.GetPublicKey(_yggdrasilOption.PrivateKey)
            };
        }

    }
}
