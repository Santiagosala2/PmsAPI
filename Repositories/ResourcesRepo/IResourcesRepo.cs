using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Resources.Models;
using Users.Models;

namespace Resources.Repo
{
    public interface IResourcesRepo
    {
        Task <bool> SaveChangesAsync();
        Task<IEnumerable<Resource>> GetAllResourcesAsync(int userId);
        Task<Resource> GetResourceByIdAsync(int id, int userId);
        Task<bool> CreateResourceAsync(int userId, Resource res);
        void UpdateResource(Resource res);
        Task<bool> DeleteResourceAsync(Resource res);
    }
}