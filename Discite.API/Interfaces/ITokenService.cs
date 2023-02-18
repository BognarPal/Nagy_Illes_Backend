using Discite.Data.Models;

namespace Discite.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserModel user);
    }
}
