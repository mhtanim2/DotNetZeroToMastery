using Microsoft.AspNetCore.Identity;

namespace PandaIdentity.Interfaces
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, IEnumerable<string> roles);
    }
}
