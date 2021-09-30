using System.Collections.Generic;
using System.Threading.Tasks;
using Resources.Models;
using Users.Models;

namespace Resources.Repo
{
    public interface IResourcesRepo
    {
        Task <bool> SaveChangesAsync();
        Task<IEnumerable<Resource>> GetAllResourcesAsync();
        Task<Resource> GetResourceByIdAsync(int id);
        Task CreateResourceAsync(Resource res);
        void UpdateResource(Resource res);
        void DeleteResource(Resource res);
    }
}