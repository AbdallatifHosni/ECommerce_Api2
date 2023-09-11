using ECommerce_Api2.Data;
using ECommerce_Api2.Models;
using ECommerce_Api2.Dtos;


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce_Api2.Services
{
    public class CategoryService : ICategoryService
    {
        EcommerceContext Context;
        public CategoryService(EcommerceContext context)
        {
            this.Context = context;
        }

        public Category create(Category item)
        {
            try
            {
                Context.Categories.Add(item);
                var result = Context.SaveChanges();
                if (result >  0)
                {
                    return item;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int delete(Category item)
        {
            Context.Categories.Remove(item);
            return Context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return Context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Category update(int id , Category item)
        {
            try
            {
                Category Categories = Context.Categories.FirstOrDefault(x => x.Id == id);
                if (Categories != null)
                {
                    Categories.Name = item.Name;
                }
                Context.Categories.Update(Categories);
                var result =  Context.SaveChanges();
                if(result > 0)
                {
                    return Categories;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public List<CategoryProductDto> CatProduct()
        {
           var cat =  Context.Categories.Include(x => x.Products);
            var res = cat.Select(x => new CategoryProductDto
            {
                CategoryId = x.Id
                ,
                ProductsNum = x.Products.Count(),
                Name = x.Name
            }).ToList();
            return res;
        }
    }
}

