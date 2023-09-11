using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce_Api2.Models
{
    public class Order
    {
        public Order()
        {
            this.CreationDate = DateTime.Now;
        }
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        [ForeignKey("User")]
        public string userId { get; set; }
        public bool IsCash { get; set; }
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

    }
}
