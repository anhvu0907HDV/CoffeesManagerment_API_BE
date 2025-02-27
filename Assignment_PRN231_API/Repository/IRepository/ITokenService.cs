using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, List<string> roles);
    }
}
