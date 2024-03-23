namespace Users.WebApi.Models
{
    public class RegisterNewAccountApiDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CreateNewPasswordMainShippingAddressApiDto MainShippingAddress { get; set; }

    }

    public class CreateNewPasswordMainShippingAddressApiDto 
    { 
        public string AddressName { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
    }
}
