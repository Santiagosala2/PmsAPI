using DataStore;
using EncryptionHelpers.Hashing;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tokens.Dtos;
using Users.Models;

namespace Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private DataStoreContext _context;
        private readonly ICustomTokenManager _customTokenManager;


        public CustomUserManager(DataStoreContext context , ICustomTokenManager customTokenManager)
        {
            _context = context;
            _customTokenManager = customTokenManager;
        }

        public async Task<(bool, string)> CreateUserAsync(User user )
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var usernameExists = await CheckUsernameExists(user.Username);

            if (!usernameExists)
            {
                var hashResult = PasswordHasher.HashPassword(user.SaltedHashedPassword);
                user.SaltedHashedPassword = hashResult[0];
                user.Salt = hashResult[1];
                IDbContextTransaction t = _context.Database.BeginTransaction();
                await _context.Users.AddAsync(user);
                int userSaved = await _context.SaveChangesAsync();
                int userId = user.UserID;
                await _context.Entities.AddAsync(new Entity { UserID = userId });
                int entitySaved = await _context.SaveChangesAsync();
                var (tokenSaved, token) = await _customTokenManager.CreateTokenAsync(userId);
                t.Commit();
                return (entitySaved == 1 && userSaved == 1 && tokenSaved, token);
            }

            return (false,"");

        }

        public async Task<(bool,string)> AuthenticateAsync(string email , string password)
        {

            if (email == null || password == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var findUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            bool authResult = false;

            if (findUser is not null)
            {
               authResult = PasswordHasher.CheckPassword(password, findUser.Salt , findUser.SaltedHashedPassword);
            }
          
            if (authResult)
            { 

                var (isRecentToken, recentToken ) = await _customTokenManager.FindRecentTokenAsync(findUser.UserID);

                if (!isRecentToken) 
                {
                    var (tokenSaved, token) = await _customTokenManager.CreateTokenAsync(findUser.UserID);
                    if (tokenSaved) return (authResult, token);             
                }

                return (authResult, recentToken ); 
            }
            
            return (false,"");

        }

        private async Task<bool> CheckUsernameExists(string userName)
        {
            bool exist = await _context.Users.AnyAsync(otherUser => otherUser.Username == userName);
            return exist;
        }
    }
}
