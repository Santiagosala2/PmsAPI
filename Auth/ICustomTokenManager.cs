using System.Threading.Tasks;
using Users.Models;

namespace Auth
{
    public interface ICustomTokenManager
    {
        Task<(bool,string)> CreateTokenAsync(int userId);
        Task<User> GetUserInfoByTokenAsync(string tokenString);
        Task<bool> VerifyTokenAsync(string token , int? userId);
        Task<(bool,string)> FindRecentTokenAsync(int userId);
    }
}