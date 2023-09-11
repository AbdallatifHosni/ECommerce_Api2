using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce_Api2.Models
{
    public class WishList
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

    }
}
