using Yggdrasil.Models.Yggdrasil;

namespace Yggdrasil.Service {
    public interface IYggdrasilConnecterService {
        ValueTask<YggdrasilUser> AuthenticateAsync(string username, string password);
        ValueTask<YggdrasilProfile[]> GetUserProfiles(Guid uuid);
        ValueTask<YggdrasilProfile> GetUserSelectedProfile(Guid uuid);
        ValueTask<YggdrasilUser> GetUser(Guid uuid);
        ValueTask<YggdrasilProfile> SetUserSelectedProfile(Guid userId, Guid uuid);
        ValueTask<YggdrasilProfile?> GetProfile(Guid uuid, bool unsigned = true);
        ValueTask<YggdrasilProfile[]> GetProfilesByNames(string[] userNames);
    }
}
