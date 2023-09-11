
using ECommerce_Api2.Models;
using ECommerce_Api2.Dtos;
using System.Collections.Generic;

namespace ECommerce_Api2.Services    
{
    public interface IOrderProductService:ICrudService<OrderProducts>
    {
        public List<ProductsCartDto> getOrderDetails(int OrderId);
    }
}
