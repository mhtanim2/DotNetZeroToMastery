using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Interface.Auth
{
    public interface ITokenService
    {
        public string CreateJWTToken(IdentityUser user, IEnumerable<string> roles);
        public string DecodeJWTToken(string tokenString);
    }
}
