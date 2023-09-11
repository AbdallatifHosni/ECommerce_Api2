using System.ComponentModel.DataAnnotations;

namespace ECommerce_Api2.Dtos
{
    public class RegesterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
