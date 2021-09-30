using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Users.Models;
using Entities.Models;
using Data;

namespace Resources.Repo
{
    public class ResourcesRepo : IResourcesRepo
    {
        private  DataContext _context;

        public ResourcesRepo(DataContext context)
        {
            _context = context;
        }

        public async Task CreateResourceAsync(Resource res)
        {
            if(res == null)
            {
                throw new ArgumentNullException(nameof(res));
            }
             
            await _context.Resources.AddAsync(res);
        }

        public void DeleteResource(Resource res)
        {
            if (res == null) 
            {
                throw new ArgumentNullException(nameof(res));
            }

           _context.Resources.Remove(res);
        }

        public async Task<IEnumerable<Resource>> GetAllResourcesAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<Resource> GetResourceByIdAsync(int id)
        {
            return await _context.Resources.FirstOrDefaultAsync(p => p.ResourceID == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ( await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateResource(Resource res)
        {
 
        }
    }
}