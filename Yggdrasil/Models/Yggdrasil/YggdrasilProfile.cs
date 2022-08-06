using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Yggdrasil.Helper;

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
        public object[] Properties { get; set; }

        public YggdrasilProfile AddBase64JsonPropertie(string name, object value) {
            var propertie = YggdrasilProfilePropertie.CreateBase64JsonPropertie(name, value);

            if (Properties is object[] properties) {
                var propertiesList = Properties.ToList();
                propertiesList.Add(propertie);
                Properties = propertiesList.ToArray();
            } else {
                var propertiesList = new List<YggdrasilProfilePropertie>();
                propertiesList.Add(propertie);
                Properties = propertiesList.ToArray();
            }

            return this;
        }

        public YggdrasilProfile AddBase64JsonPropertie(string name, object value, string privateKey) {
            var propertie = YggdrasilProfilePropertieSign.CreateBase64JsonPropertieSign(name, value, privateKey);

            if (Properties is object[] properties) {
                var propertiesList = Properties.ToList();
                propertiesList.Add(propertie);
                Properties = propertiesList.ToArray();
            } else {
                var propertiesList = new List<YggdrasilProfilePropertie>();
                propertiesList.Add(propertie);
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
        /// <returns><see cref="YggdrasilProfilePropertie"/></returns>
        public static YggdrasilProfilePropertie CreateBase64JsonPropertie(string name, object value) {
            var json = JsonSerializer.Serialize(value);
            return new YggdrasilProfilePropertie { Name = name, Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(json)) };
        }
    }

    /// <summary>
    /// 角色属性带签名
    /// </summary>
    public class YggdrasilProfilePropertieSign : YggdrasilProfilePropertie {
        /// <summary>
        /// 角色属性签名
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// 创建一个 <see cref="YggdrasilProfilePropertieSign"/>
        /// </summary>
        /// <param name="name">角色属性名称</param>
        /// <param name="value">角色属性值</param>
        /// <returns><see cref="YggdrasilProfilePropertie"/></returns>
        public static YggdrasilProfilePropertieSign CreateBase64JsonPropertieSign(string name, object value, string privateKey) {
            var json = JsonSerializer.Serialize(value);
            var text = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            return new YggdrasilProfilePropertieSign {
                Name = name,
                Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(json)),
                Signature = RSAHelper.Sign(text, privateKey)
            };
        }
    }
}
