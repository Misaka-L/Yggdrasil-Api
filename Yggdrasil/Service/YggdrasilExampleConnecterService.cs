using System;
using Yggdrasil.Models.Yggdrasil;

namespace Yggdrasil.Service {
    public class YggdrasilExampleConnecterService : IYggdrasilConnecterService {
        public async ValueTask<YggdrasilUser> AuthenticateAsync(string username, string password) {
            return new YggdrasilUser {
                Id = Guid.NewGuid()
            };
        }

        public async ValueTask<YggdrasilProfile?> GetProfile(Guid uuid) {
            return new YggdrasilProfile { Id = uuid, Name = "test" }.AddBase64JsonPropertie("textures", new YggdrasilProfileTexture {
                ProfileId = uuid,
                ProfileName = "test"
            }.AddTexture("SKIN", new YggdrasilSkinTextures {
                Url = "https://littleskin.cn/textures/341318ac2169d09bf02c27601172c76873265a5031e60148ce2768e4a72b859f",
                MetaData = new SkinYggdrasilTextureMetaData {
                    Model = "slim"
                }
            }));
        }

        public async ValueTask<YggdrasilProfile[]> GetProfilesByNames(string[] userNames) {
            var profiles = new List<YggdrasilProfile>();
            foreach (var userName in userNames) {
                profiles.Add(new YggdrasilProfile { Id = Guid.NewGuid(), Name = userName });
            };

            return profiles.ToArray();
        }

        public async ValueTask<YggdrasilUser> GetUser(Guid uuid) {
            return new YggdrasilUser {
                Id = uuid
            };
        }

        public async ValueTask<YggdrasilProfile[]> GetUserProfiles(Guid uuid) {
            return new YggdrasilProfile[] {
                new YggdrasilProfile { Id = uuid, Name = "test" }
            };
        }

        public async ValueTask<YggdrasilProfile> GetUserSelectedProfile(Guid uuid) {
            return new YggdrasilProfile { Id = uuid, Name = "test" };
        }

        public async ValueTask<YggdrasilProfile> SetUserSelectedProfile(Guid userId, Guid uuid) {
            return new YggdrasilProfile { Id = uuid, Name = "test" };
        }
    }
}
