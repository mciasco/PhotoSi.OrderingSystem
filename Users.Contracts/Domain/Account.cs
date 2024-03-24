using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Contracts.Domain
{
    public class Account
    {
        protected Account()
        {
        }

        private static bool IsValidEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
                return false;

            var trimmedEmail = email.Trim();
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static Account Create(
            string name,
            string surname,
            string registrationEmail,
            string username,
            string passwordHash)
        {
            if (!IsValidEmail(registrationEmail))
                throw new ArgumentException("Invalid registration email");

            return new Account()
            {
                AccountId = Guid.NewGuid().ToString(),
                Name = name,
                Surname = surname,
                RegistrationEmail = registrationEmail,
                Username = username,
                PasswordHash = passwordHash
            };
        }


        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
