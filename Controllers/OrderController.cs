using ECommerce_Api2.Dtos;
using ECommerce_Api2.Models;
using ECommerce_Api2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AngEcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProductService _OrderProduct;
        private readonly IOrderService _Order;
        private readonly UserManager<User> userManger;

        public OrderController(IOrderProductService orderProduct , IOrderService order,
            UserManager<User> userManger)
        {
            this._Order = order;
            this.userManger = userManger;
            this._OrderProduct = orderProduct;
        }
        [HttpGet("detail")]
        public IActionResult getOrder(int id)
        {
            var result = _OrderProduct.getOrderDetails(id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult postOrde(string id , [FromBody]List<ProductsCartDto> list)
        {
            try
            {
                
                var order = new Order();
                order.userId = id;
                var responseMessage = new OrderMessage { IsOk = false, productsFailed = new List<string>() };
                order = this._Order.create(order);
                foreach (var item in list)
                {
                    var newOrPro = new OrderProducts
                    {
                        OrderId = order.Id,
                        ProductId = item.Id,
                        ProductQuantity = item.Quantity,
                        ProductTotalPrice = item.Price * item.Quantity

                    };
                    var res = this._OrderProduct.create(newOrPro);
                    if (res == null)
                    {
                        responseMessage.productsFailed.Add(item.Name);
                    }
                }
                return Ok(responseMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }

    }
}
