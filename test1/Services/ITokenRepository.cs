using Microsoft.AspNetCore.Identity;

namespace test1.Services
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }

}
