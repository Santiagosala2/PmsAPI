using DataStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tokens.Models;
using Users.Models;

namespace Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {

        private DataStoreContext _context;

        public CustomTokenManager(DataStoreContext context )
        {
            _context = context;
        }

        public async Task<(bool,string)> CreateTokenAsync(int userId)
        {
            var token = new TokenReadDto(userId);
            await _context.Tokens.AddAsync(token);
            int savedToken =  await _context.SaveChangesAsync();
            return  (savedToken == 1 , token.TokenString);
        }

        public async Task<bool> VerifyTokenAsync(string token , int? userId)
        {
            return await _context.Tokens.AnyAsync(x => x != null && token.Contains(x.TokenString) && x.ExpiryDate >
            DateTime.Now && ((userId != null || userId > 0 ) ? (x.UserID == userId) : true));
        }

        public async Task<User> GetUserInfoByTokenAsync(string tokenString)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(x => tokenString != null && tokenString.Contains(x.TokenString) && x.ExpiryDate >
            DateTime.Now);
            if (token is not null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID== token.UserID);
                return user;
            }      
            return null;
        }
    }
}
