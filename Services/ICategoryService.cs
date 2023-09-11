using ECommerce_Api2.Dtos;
using ECommerce_Api2.Models;
using System.Collections.Generic;

namespace ECommerce_Api2.Services 
{
    public interface ICategoryService :ICrudService<Category>
    {
        public List<CategoryProductDto> CatProduct();
    }
}
