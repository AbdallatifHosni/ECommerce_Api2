using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECommerce_Api2.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Order { get; set; }

    }
}
