using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_Api2.Dtos
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quentity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        public DateTime CreatedDate { get; set; }
        public string  imagePath { get; set; }

    }
}
