using System.Text.Json.Serialization;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 带用户名, 密码的请求
    /// </summary>
    public class LoginRequest {
        /// <summary>
        /// 邮箱或其他凭证
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
