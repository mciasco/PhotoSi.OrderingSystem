using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Contracts.Domain
{
    public class Account
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
