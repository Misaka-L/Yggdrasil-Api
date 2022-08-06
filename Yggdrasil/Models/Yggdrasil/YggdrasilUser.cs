using System.Text.Json.Serialization;

namespace Yggdrasil.Models.Yggdrasil {
    /// <summary>
    /// Yggdrasil 用户对象
    /// </summary>
    public class YggdrasilUser {
        /// <summary>
        /// 用户 UUID（无符号）
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 用户属性
        /// </summary>
        [JsonPropertyName("properties")]
        public YggdrasilUserPropertie[] Properties { get; set; }
    }

    /// <summary>
    /// Yggdrasil 用户属性
    /// </summary>
    public class YggdrasilUserPropertie {
        /// <summary>
        /// 属性名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
