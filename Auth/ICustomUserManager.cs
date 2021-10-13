using System.Threading.Tasks;
using Users.Models;

namespace Auth
{
    public interface ICustomUserManager
    {
        Task<(bool, string)> AuthenticateAsync(string userName, string password);
        Task<(bool, string)> CreateUserAsync(User user);
    }
}