using ECommerce_Api2.Models;
using ECommerce_Api2.Data;
using ECommerce_Api2.Services;

using System.Collections.Generic;

namespace ECommerce_Api2.Services
{
    public class WishProductService : IwishProductService
    {
        EcommerceContext Context;
        public WishProductService(EcommerceContext context)
        {
            this.Context = context;
        }
        public WishProducts create(WishProducts item)
        {
            throw new System.NotImplementedException();
        }

        public int delete(WishProducts item)
        {
            throw new System.NotImplementedException();
        }

        public List<WishProducts> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public WishProducts GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public WishProducts update(int id, WishProducts item)
        {
            throw new System.NotImplementedException();
        }
    }
}
