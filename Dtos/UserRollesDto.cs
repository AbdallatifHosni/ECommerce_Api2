using System.Collections.Generic;

namespace ECommerce_Api2.Dtos
{
    public class UserRollesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }



    }
}
