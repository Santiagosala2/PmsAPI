using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionHelpers.Hashing
{
    public class PasswordHasher
    {
        public static List<string> HashPassword (string password)
        {

            var result = new List<string>();

            if (password is not null)
            {
                // generate a reandom salt
                var rng = RandomNumberGenerator.Create();
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                var saltText = Convert.ToBase64String(saltBytes);

                // generate the salted and hashed password

                var saltedhashedPassword = SaltAndHashPassword(password, saltText);
                result.Add(saltedhashedPassword);
                result.Add(saltText);
            }

            return result;
        }

        public static bool CheckPassword(string password, string userSalt , string originalSaltedPassword)
        {
            // re-generate the salted and hashed password
            if (password is not null)
            {
                var saltedhashedPassword = SaltAndHashPassword(password, userSalt);
                return (saltedhashedPassword == originalSaltedPassword);
            }
            return false;
        }

        private static string SaltAndHashPassword(string password , string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }
    }
}
