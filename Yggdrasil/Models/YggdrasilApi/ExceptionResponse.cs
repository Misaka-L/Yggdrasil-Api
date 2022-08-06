using System.Text.Json.Serialization;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// 错误信息响应
    /// </summary>
    public class ExceptionResponse {
        /// <summary>
        /// 错误的简要描述（机器可读）
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }
        /// <summary>
        /// 错误的详细信息（人类可读）
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 该错误的原因（可选）
        /// </summary>
        [JsonPropertyName("cause")]
        public string? Cause { get; set; }
    }
}
