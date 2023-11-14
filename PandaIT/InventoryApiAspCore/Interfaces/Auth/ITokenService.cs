using Microsoft.AspNetCore.Identity;

namespace InventoryApiAspCore.Interfaces.Auth
{
    public interface ITokenService
    {
        public string CreateJWTToken(IdentityUser user, IEnumerable<string> roles);
        public string DecodeJWTToken(string tokenString);
    }
}
