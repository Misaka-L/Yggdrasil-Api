using System.Text.Json.Serialization;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 加入服务器请求
    /// </summary>
    public class JoinServerRequest : TokenRequestResponse {
        /// <summary>
        /// 该令牌绑定的角色的 UUID（无符号）
        /// </summary>
        [JsonPropertyName("selectedProfile")]
        public string SelectedProfile { get; set; }
        /// <summary>
        /// 服务端发送给客户端的 serverId
        /// </summary>
        [JsonPropertyName("serverId")]
        public string ServerId { get; set; }
    }
}
