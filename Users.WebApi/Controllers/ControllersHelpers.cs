using Users.Contracts.Domain;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    public static class ControllersHelpers
    {
        public static AccountApiDto ToApiDto(this Account account)
        {
            return new AccountApiDto()
            {
                AccountId = account.AccountId,
                Name = account.Name,
                RegistrationEmail = account.RegistrationEmail,
                Surname = account.Surname,
                Username = account.Username,
            };
        }
    }
}
