using Microsoft.AspNetCore.Identity;

namespace Test.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);  
    }
}
