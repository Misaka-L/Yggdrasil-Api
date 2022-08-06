using System.Text.Json.Serialization;

namespace Yggdrasil.Models.Yggdrasil {
    /// <summary>
    /// Yggdrasil 材质
    /// </summary>
    public class YggdrasilTexture {
        /// <summary>
        /// 材质 Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class YggdrasilSkinTextures : YggdrasilTexture {
        /// <summary>
        /// 材质元数据
        /// </summary>
        [JsonPropertyName("metadata")]
        public SkinYggdrasilTextureMetaData MetaData { get; set; }
    }

    /// <summary>
    /// Yggdrasil 皮肤材质元数据
    /// </summary>
    public class SkinYggdrasilTextureMetaData {
        [JsonPropertyName("model")]
        public string Model { get; set; }
    }
}