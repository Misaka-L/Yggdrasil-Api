using System.Text.Json.Serialization;
using Yggdrasil.Models.Yggdrasil;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 验证响应
    /// </summary>
    public class AuthenticateResponse : TokenRequestResponse {
        /// <summary>
        /// 可用角色列表
        /// </summary>
        [JsonPropertyName("availableProfiles")]
        public YggdrasilProfile[] AvailableProfiles { get; set; }
        /// <summary>
        /// 绑定的角色
        /// </summary>
        [JsonPropertyName("selectedProfile")]
        public YggdrasilProfile SelectedProfile { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        [JsonPropertyName("user")]
        public YggdrasilUser User { get; set; }
    }
}
