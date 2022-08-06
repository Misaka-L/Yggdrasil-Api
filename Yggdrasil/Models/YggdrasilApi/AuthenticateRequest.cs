using System.Text.Json.Serialization;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 验证请求
    /// </summary>
    public class AuthenticateRequest : LoginRequest {
        /// <summary>
        /// ClientToken
        /// </summary>
        [JsonPropertyName("clientToken")]
        public string? ClientToken { get; set; }
        /// <summary>
        /// 是否请求用户信息
        /// </summary>
        [JsonPropertyName("requestUser")]
        public bool RequestUser { get; set; } = false;
        /// <summary>
        /// Agent
        /// </summary>
        [JsonPropertyName("agent")]
        public AuthenticateRequestAgent Agent { get; set; }
    }

    /// <summary>
    /// Agent
    /// </summary>
    public class AuthenticateRequestAgent {
        /// <summary>
        /// 验证应用名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = "Minecraft";
        /// <summary>
        /// 验证版本
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; } = 1;
    }
}
