using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Yggdrasil.Models.Yggdrasil {
    /// <summary>
    /// Yggdrasil 角色材质
    /// </summary>
    public class YggdrasilProfileTexture {
        /// <summary>
        /// 该属性值被生成时的时间戳（Java 时间戳格式，即自 1970-01-01 00:00:00 UTC 至今经过的毫秒数）
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        /// <summary>
        /// 角色 UUID（无符号）
        /// </summary>
        [JsonPropertyName("profileId")]
        public Guid ProfileId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonPropertyName("profileName")]
        public string ProfileName { get; set; }
        /// <summary>
        /// 角色材质
        /// </summary>
        [JsonPropertyName("textures")]
        public JsonObject Textures { get; set; } = new JsonObject();

        public YggdrasilProfileTexture AddTexture(string type, YggdrasilTexture texture) {
            Textures[type] = JsonNode.Parse(JsonSerializer.Serialize(texture));
            return this;
        }

        public YggdrasilProfileTexture AddTexture(string type, YggdrasilSkinTextures texture) {
            Textures[type] = JsonNode.Parse(JsonSerializer.Serialize(texture));
            return this;
        }
    }
}
