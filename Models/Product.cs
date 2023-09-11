using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce_Api2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.0, Int64.MaxValue, ErrorMessage = "Quentity must be greater than 0.")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
        [Required]
        public string  ImagePath { get; set; }
        [Required]
        [ForeignKey("category")]
        public int CategoryId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public Category category { get; set; }
    }
}
