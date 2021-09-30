using System.Threading.Tasks;
using Users.Models;

namespace Users.Repo
{
    public interface IUsersRepo
    {
        Task<bool> CreateUserAsync(User res);
    }
}