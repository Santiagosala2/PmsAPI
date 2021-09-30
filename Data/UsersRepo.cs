using Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Repo
{
    public class UsersRepo : IUsersRepo
    {
        private DataContext _context;

        public UsersRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(User res)
        {

            if (res == null)
            {
                throw new ArgumentNullException(nameof(res));
            }

            var usernameExists = await CheckIfUsernameExists(res);

            if (!usernameExists)
            {
                IDbContextTransaction t = _context.Database.BeginTransaction();
                await _context.Users.AddAsync(res);
                int userSaved = await _context.SaveChangesAsync();
                int userId = res.UserID;
                await _context.Entities.AddAsync(new Entity { UserID = userId });
                int entitySaved = await _context.SaveChangesAsync();
                t.Commit();
                return (entitySaved == 1 && userSaved == 1);
            }

            return false;

        }

        private async Task<bool> CheckIfUsernameExists(User currentUser)
        {
            bool exist = await _context.Users.AnyAsync(otherUser => otherUser.Username == currentUser.Username);
            return exist;
        }
    }
}
