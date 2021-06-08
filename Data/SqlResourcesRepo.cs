using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Models;

namespace Resources.Data
{
    public class SqlResourcesRepo : IResourcesRepo
    {
        private ResourcesContext _context;

        public SqlResourcesRepo(ResourcesContext context)
        {
            _context = context;
        }

        public void CreateResource(Resource res)
        {
            if(res == null)
            {
                throw new ArgumentNullException(nameof(res));
            }
             
            Console.WriteLine(res);

            _context.Resources.Add(res);
        }

        public void DeleteResource(Resource res)
        {
            if (res == null) 
            {
                throw new ArgumentNullException(nameof(res));
            }

            _context.Resources.Remove(res);
        }

        public IEnumerable<Resource> GetAllResources()
        {
            return _context.Resources.ToList();
        }

        public Resource GetResourceById(int id)
        {
            return _context.Resources.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateResource(Resource res)
        {
            
        }
    }
}