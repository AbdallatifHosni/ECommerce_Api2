using ECommerce_Api2.Models;
using ECommerce_Api2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngEcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryRepository;
        public CategoryController(ICategoryService categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet("{id:int}", Name = "GetOneCategory")]
        public IActionResult GetById(int id)
        {
            Category product = categoryRepository.GetById(id);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categoryRepository.GetAll());
        }
        [HttpPost]
        public IActionResult Insert(Category product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var resu =  categoryRepository.create(product);
                    string url = Url.Link("GetOneCategory", new { id = resu.Id });
                    return Created(url, resu);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Category product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categoryRepository.update(id, product);

                    return StatusCode(204, "Data Saved");// Created(url, dep);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("catProduct")]
        public IActionResult getCatProduct()
        {
            var res = this.categoryRepository.CatProduct();

            return Ok(res);
        }
    }
}
