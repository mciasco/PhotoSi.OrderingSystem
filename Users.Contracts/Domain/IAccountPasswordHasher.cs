using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Contracts.Domain
{
    public interface IAccountPasswordHasher
    {
        Task<string> HashPassword(string password);
    }
}
