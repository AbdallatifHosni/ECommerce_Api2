using System.Collections.Generic;

namespace ECommerce_Api2.Dtos
{
    public class OrderMessage
    {
        public bool IsOk { get; set; }
        public List<string> productsFailed { get; set; }
    }
}
