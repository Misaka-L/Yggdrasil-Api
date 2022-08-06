using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yggdrasil.Models.Yggdrasil {
    /// <summary>
    /// Yggdrasil 角色
    /// </summary>
    public class YggdrasilProfile {
        /// <summary>
        /// 角色 UUID（无符号）
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// 角色属性
        /// </summary>
        [JsonPropertyName("properties")]
        public YggdrasilProfilePropertie[] Properties { get; set; }

        public YggdrasilProfile AddBase64JsonPropertie(string name, object value, bool isSign = false) {
            if (Properties is YggdrasilProfilePropertie[] properties) {
                var propertiesList = Properties.ToList();
                propertiesList.Add(YggdrasilProfilePropertie.CreateBase64JsonPropertie(name, value, isSign));
                Properties = propertiesList.ToArray();
            } else {
                var propertiesList = new List<YggdrasilProfilePropertie>();
                propertiesList.Add(YggdrasilProfilePropertie.CreateBase64JsonPropertie(name, value, isSign));
                Properties = propertiesList.ToArray();
            }

            return this;
        }
    }

    /// <summary>
    /// 角色属性
    /// </summary>
    public class YggdrasilProfilePropertie {
        /// <summary>
        /// 角色属性名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// 角色属性值
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// 创建一个 <see cref="YggdrasilProfilePropertie"/>
        /// </summary>
        /// <param name="name">角色属性名称</param>
        /// <param name="value">角色属性值</param>
        /// <param name="isSign">是否签名</param>
        /// <returns><see cref="YggdrasilProfilePropertie"/></returns>
        public static YggdrasilProfilePropertie CreateBase64JsonPropertie(string name, object value, bool isSign) {
            var json = JsonSerializer.Serialize(value);
            return new YggdrasilProfilePropertie { Name = name, Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(json)) };
        }
    }

    /// <summary>
    /// 角色属性带签名
    /// </summary>
    public class YggdrasilProfilePropertieSign {
        /// <summary>
        /// 角色属性签名
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}
