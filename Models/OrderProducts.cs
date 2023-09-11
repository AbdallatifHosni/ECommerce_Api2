using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace  ECommerce_Api2.Models
{
    public class OrderProducts
    {

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductTotalPrice { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }

    }
}
