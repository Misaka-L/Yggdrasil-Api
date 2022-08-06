using System.Text.Json.Serialization;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 带令牌的响应/请求
    /// </summary>
    public class TokenRequestResponse {
        /// <summary>
        /// AccessToken
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        /// <summary>
        /// ClientToken
        /// </summary>
        [JsonPropertyName("clientToken")]
        public string? ClientToken { get; set; }
    }
}
