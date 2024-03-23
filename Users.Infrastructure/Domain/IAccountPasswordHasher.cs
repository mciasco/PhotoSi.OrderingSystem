using System.Security.Cryptography;
using System.Text;

namespace Users.Contracts.Domain
{
    public class MD5AccountPasswordHasher : IAccountPasswordHasher
    {
        public async Task<string> HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(hashValue);
            }
        }
    }
}
