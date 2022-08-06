using System.Text.Json.Serialization;
using Yggdrasil.Models.Yggdrasil;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 刷新请求
    /// </summary>
    public class RefreshRequest : TokenRequestResponse {
        /// <summary>
        /// 选择的 Yggdrasil 角色
        /// </summary>
        [JsonPropertyName("selectedProfile")]
        public YggdrasilProfile SelectedProfile { get; set; }
    }
}
