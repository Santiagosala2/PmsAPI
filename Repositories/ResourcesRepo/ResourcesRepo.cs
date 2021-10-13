using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using DataStore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using Accounts.Models;
using Entities.Models;

namespace Resources.Repo
{
    public class ResourcesRepo : IResourcesRepo
    {
        private  DataStoreContext _context;

        public ResourcesRepo(DataStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateResourceAsync(int userId , Resource res)
        {
            if(res == null )
            {
                throw new ArgumentNullException(nameof(res));
            }
   
            var entity = await FindUserEntity(userId);

            if (entity != null)
            {

                IDbContextTransaction t = _context.Database.BeginTransaction();
                await _context.Resources.AddAsync(res);
                int savedResource = await _context.SaveChangesAsync();
                await _context.Accounts.AddAsync(new Account
                {
                    ResourceID = res.ResourceID,
                    EntityID = entity.EntityID
                });
                int savedAccount = await _context.SaveChangesAsync();
                t.Commit();
                return (savedResource == 1 && savedAccount == 1);

            }

            return (false);
            
        }

        public async Task<bool> DeleteResourceAsync(Resource res)
        {
            if (res == null) 
            {
                throw new ArgumentNullException(nameof(res));
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.ResourceID == res.ResourceID);

            if (account != null)
            {
                IDbContextTransaction t = _context.Database.BeginTransaction();
                _context.Resources.Remove(res);
                _context.Accounts.Remove(account);
                t.Commit();
                return true;
            }
            return false;
          
        }

        public async Task<IEnumerable<Resource>> GetAllResourcesAsync(int userId)
        {
            var entity = await FindUserEntity(userId);
            IEnumerable<Resource> resources = new List<Resource>();
            if (entity is not null)
            {
                int entityId = entity.EntityID;

                resources = await _context.Accounts
                    .Where(a => a.EntityID == entityId)
                    .Join(inner: _context.Resources,
                          outerKeySelector: account => account.ResourceID,
                          innerKeySelector: resource => resource.ResourceID,
                          resultSelector: (a, r) =>
                          new Resource {
                              ResourceID = r.ResourceID,
                              Email = r.Email,
                              Password =  r.Password,
                              Account = r.Account,
                              Website = r.Website })
                    .ToListAsync();
            }
            return resources;
        }

        public async Task<Resource> GetResourceByIdAsync(int id, int userId)
        {
            var userResources = await GetAllResourcesAsync(userId);

            return userResources.FirstOrDefault(p => p.ResourceID == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
           

            return ( await _context.SaveChangesAsync() >= 0);
        }

        private async Task<Entity> FindUserEntity(int userId)
        {
            var entity = await _context.Entities.FirstOrDefaultAsync(e => e.UserID == userId);
            return entity;
        }

        public void UpdateResource(Resource res)
        {
            _context.Entry(res).State = EntityState.Modified;
        }
    }
}