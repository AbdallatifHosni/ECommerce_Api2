
using ECommerce_Api2.Models;
using ECommerce_Api2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce_Api2.Dtos;

namespace ECommerce_Api2.Services
{
    public class ProductService : IProductService
    {
        EcommerceContext Context;
        public ProductService(EcommerceContext context)
        {
            this.Context = context;
        }
        public Product create(Product item)
        {
            try
            {
                Context.Products.Add(item);
                var result = Context.SaveChanges();
                if (result > 0)
                {
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int delete(Product item)
        {
            Context.Products.Remove(item);
            return Context.SaveChanges();
        }

        public List<Product> Filter(FilteredProduct product)
        {
            var result = GetAll();
            result = result.Skip(product.Page * product.Size).Take(product.Size).ToList();
            if (product.Filter != null)
            {
             result = result.FindAll(x => (x.Name.Any(i => product.Filter.Contains(i)))).ToList();
            }
            if(product.Order != "asc")
            {
               result.Reverse();
            }
            
            return result;
        }

        public List<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return Context.Products.FirstOrDefault(x => x.Id == id);
        }
        public List<Product> GetProductByCategoryID(int CategoryId)
        {
            var res = Context.Products.Where(x => (CategoryId > 0)?x.Id == CategoryId:true).ToList();
            return res; 
        }

        public Product newUpdate(int id, ProductDto product)
        {
            try
            {
                var product1 = GetById(id);
                if (product1 != null)
                {
                    product1.CategoryId = product.CategoryId;
                    product1.Price = product.Price;
                    product1.Quantity = product.Quentity;
                    product1.Name = product.Name;
                    product1.ImagePath = product.imagePath;
                    int re = Context.SaveChanges();
                    if(re > 0)
                        return product1;
                    return null;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
            }

            public Product update(int id, Product item)
            {
            try
            {
                var pro = this.GetById(id);
                pro.Id = item.Id;
                pro.Name = item.Name;
                pro.Quantity = item.Quantity;
                pro.Price =item.Price;
                Context.Products.Update(pro);
                Context.SaveChanges();
                return pro;
            }
            catch(Exception ex)
            {
                return null;
            }
            }

    }
}
