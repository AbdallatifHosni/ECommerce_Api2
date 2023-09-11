using System.ComponentModel.DataAnnotations;

namespace ECommerce_Api2.Dtos
{
    public class AddRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
