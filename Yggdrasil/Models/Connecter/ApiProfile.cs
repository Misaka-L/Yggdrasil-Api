using Yggdrasil.Models.Yggdrasil;

namespace Yggdrasil.Models.Connecter {
    public class ApiProfile {
        public Guid uuid { get; set; }
        public string name { get; set; }
        public string skinUrl { get; set; }
        public string skinModel { get; set; }
        public string capeUrl { get; set; }

        public YggdrasilProfile ToYggdrasilProfile() {
            var profile = new YggdrasilProfile {
                Id = uuid,
                Name = name
            };

            var texture = new YggdrasilProfileTexture {
                ProfileId = uuid,
                ProfileName = name,
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            };

            texture.AddTexture("SKIN",
                new YggdrasilSkinTextures {
                    MetaData = new SkinYggdrasilTextureMetaData { Model = skinModel },
                    Url = skinUrl
                });

            if (!string.IsNullOrEmpty(capeUrl)) {
                texture.AddTexture("CAPE", new YggdrasilTexture {
                    Url = capeUrl
                });
            }

            profile.AddBase64JsonPropertie("textures", texture);
            return profile;
        }

        public YggdrasilProfile ToYggdrasilProfileSign(string privateKey) {
            var profile = new YggdrasilProfile {
                Id = uuid,
                Name = name
            };

            var texture = new YggdrasilProfileTexture {
                ProfileId = uuid,
                ProfileName = name,
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
            };

            texture.AddTexture("SKIN",
                new YggdrasilSkinTextures {
                    MetaData = new SkinYggdrasilTextureMetaData { Model = skinModel },
                    Url = skinUrl
                });

            if (!string.IsNullOrEmpty(capeUrl)) {
                texture.AddTexture("CAPE", new YggdrasilTexture {
                    Url = capeUrl
                });
            }

            profile.AddBase64JsonPropertie("textures", texture, privateKey);
            return profile;
        }
    }
}
