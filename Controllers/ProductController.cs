using ECommerce_Api2.Dtos;
using ECommerce_Api2.Models;
using ECommerce_Api2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AngEcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productRepository;
        private readonly IHttpContextAccessor acce;
        private readonly IWebHostEnvironment _env;


        public ProductController(IProductService productRepository, IHttpContextAccessor _Acce,
            IWebHostEnvironment env)
        {
            this.productRepository = productRepository;
            acce = _Acce;
            _env = env;
        }
        [HttpGet]
        public IActionResult GetByCategorryId(int cateogry = -1)
         {
            var products = productRepository.GetProductByCategoryID(cateogry);
            if (products != null)
            {   
                return Ok(products);
            }
            return BadRequest();
        }
        [HttpGet("filter")]
        public IActionResult GetFiltered([FromQuery] FilteredProduct pro)
        {
            var products = productRepository.Filter(pro);
            if (products != null)
            {
                return Ok(products);
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                var res = productRepository.delete(product);
                return Ok(product);

                    
             }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPatch]
        public async Task<IActionResult>update(int id,[FromForm] ProductDto product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Image = product.file;
                    string imagePath = await uploadImage(Image);
                    product.imagePath = imagePath;
                    var res = productRepository.newUpdate(id, product);
                    if (res != null)
                    {
                        string url = Url.Link("GetOneProduct", new { id });

                        return Created(url, product);
                    }
                    return BadRequest("Not Found");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);


        }
        
        [HttpGet("{id:int}", Name = "GetOneProduct")]
        public IActionResult GetById(int id)
        {
            Product product = productRepository.GetById(id);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();
        }
        
        //[HttpDelete]
        //public IActionResult delete()
        //{

        //}

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] ProductDto product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Image = product.file;
                    Product product1 = new();
                    product1.ImagePath = await uploadImage(Image);
                    product1.CategoryId = product.CategoryId;
                    product1.Price = product.Price;
                    product1.Quantity = product.Quentity;
                    product1.Name = product.Name;
                   var res =  productRepository.create(product1);
                    string url = Url.Link("GetOneProduct", new { id = res.Id });
                    return Created(url, product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);
        }

        private async Task<string> uploadImage(IFormFile image)
        {
            var uniqueFileName = GetUniqueFileName(image.FileName);
            var dir = Path.Combine(_env.WebRootPath, "Images");
           
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, uniqueFileName);
            await image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            var baseUrl = acce.HttpContext.Request.Scheme + "://" +
                 acce.HttpContext.Request.Host +
                 acce.HttpContext.Request.PathBase;
            return baseUrl +"/Images/"+ uniqueFileName;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString()
                   + Path.GetExtension(fileName);
        }
    }
}
