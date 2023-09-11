using ECommerce_Api2.Dtos;
using ECommerce_Api2.Data;
using ECommerce_Api2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce_Api2.Services
{
    public class OrderProdutsService : IOrderProductService
    {

        
        EcommerceContext Context;
        IProductService productRepository;
        IOrderService OrderRepository;
        public OrderProdutsService(EcommerceContext context, IProductService productRepository
            ,IOrderService OrderRepository)
        {
            Context = context;
            this.productRepository = productRepository;
            this.OrderRepository = OrderRepository;
        }
        public OrderProducts create(OrderProducts item)
        {
            try
            {
                var availableQuantity = Context.Products.Where(x => x.Id == item.ProductId).Select(x => x.Quantity).First();
                if (availableQuantity >= item.ProductQuantity)
                {
                    var pro = this.productRepository.GetById(item.ProductId);
                    pro.Quantity -= item.ProductQuantity;
                    this.productRepository.update(item.ProductId, pro);
                    this.Context.OrderProducts.Add(item);
                    var order = this.OrderRepository.GetById(item.OrderId);
                    order.TotalPrice += item.ProductTotalPrice;
                    this.OrderRepository.update(item.OrderId, order);
                    var re = Context.SaveChanges();
                    if (re > 0)
                        return item;
                    return null;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int delete(OrderProducts item)
        {
            try
            {
                var product = productRepository.GetById(item.ProductId);
                product.Quantity += item.ProductQuantity;
                productRepository.update(item.ProductId, product);
                var order = this.OrderRepository.GetById(item.OrderId);
                order.TotalPrice -= item.ProductTotalPrice;
                this.OrderRepository.update(item.OrderId,order);
                Context.OrderProducts.Remove(item);
                return Context.SaveChanges();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public List<OrderProducts> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderProducts GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductsCartDto> getOrderDetails(int OrderId)
        {
            try
            {
                var result =
                        (from OrderP in Context.OrderProducts
                         join Pro in Context.Products on OrderP.ProductId equals Pro.Id
                         select new ProductsCartDto
                         {
                             Id = Pro.Id,
                             Name = Pro.Name,
                             Image = Pro.ImagePath,
                             Price = Pro.Price,
                             Quantity = OrderP.ProductQuantity,
                         }).ToList();
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }

            }

        public OrderProducts update(int id, OrderProducts item)
        {
            try
            {
                var old = Context.OrderProducts.FirstOrDefault(x => x.OrderId == item.OrderId && x.ProductId == item.ProductId);
                if (old != null)
                {
                    Context.OrderProducts.Remove(old);
                    var availableQuantity = Context.Products.Where(x => x.Id == old.ProductId).Select(x => x.Quantity).First();
                    if (availableQuantity >= item.ProductQuantity)
                    {
                        var product = productRepository.GetById(item.ProductId);
                        product.Quantity -= item.ProductQuantity;
                        productRepository.update(item.ProductId, product);
                        old.ProductQuantity = item.ProductQuantity;
                        var order = this.OrderRepository.GetById(item.OrderId);
                        order.TotalPrice -= old.ProductTotalPrice;
                        old.ProductTotalPrice = item.ProductQuantity * product.Price;
                        order.TotalPrice += old.ProductTotalPrice;
                        this.OrderRepository.update(item.OrderId, order);
                        Context.SaveChanges();
                        return old;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
