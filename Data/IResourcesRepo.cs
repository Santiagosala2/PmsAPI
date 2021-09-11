using System.Collections.Generic;
using System.Threading.Tasks;
using Resources.Models;

namespace Resources.Data
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