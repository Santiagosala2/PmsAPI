using System.Collections.Generic;
using Resources.Models;

namespace Resources.Data
{
    public interface IResourcesRepo
    {
        bool SaveChanges();
        IEnumerable<Resource> GetAllResources();
        Resource GetResourceById(int id);
        void CreateResource(Resource res);

        void UpdateResource(Resource res);
        void DeleteResource(Resource res);

    }
}