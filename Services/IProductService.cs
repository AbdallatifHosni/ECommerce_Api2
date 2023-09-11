using ECommerce_Api2.Dtos;
using ECommerce_Api2.Models;
using System.Collections.Generic;

namespace ECommerce_Api2.Services
{
    public interface IProductService :ICrudService<Product>
    {
        public List<Product> GetProductByCategoryID(int CategoryId);
        public List<Product> Filter(FilteredProduct product);

        public Product newUpdate(int id , ProductDto product);

    }
}
