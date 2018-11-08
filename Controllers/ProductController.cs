using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAPI.Database;
using MyAPI.Models;
namespace MyAPI.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        ILogger<ProductController> _logger;
        private readonly DatabaseContext Context;

        public ProductController(ILogger<ProductController> logger, DatabaseContext Context)
        {
            this.Context = Context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Products> data = Context.Products.ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute GET");
                return BadRequest();
            }
        }

        [HttpGet("{product_id}", Name = "GetProduct")]
        public IActionResult Get(int product_id)
        {
            try
            {
                Products data = Context.Products.SingleOrDefault(p => p.ProductId == product_id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute GET");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Products model)
        {
            try
            {
                Context.Add(model);
                Context.SaveChanges();
                //return Created("GetProduct", new { product_id = model.ProductId});  //201 Insert แล้ว ให้ส่งข้อมูลกลับไปด้วย
                return CreatedAtRoute("GetProduct", new { product_id = model.ProductId},model);  //ส่งข้อมูลทั้งก่อน กลับไป 
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute POST");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Products model, int id)
        {
            try
            {
                Products data = Context.Products.SingleOrDefault(p => p.ProductId == id);
                if (data == null)
                {
                    return NotFound();
                }

                data.Name = model.Name;
                data.Detail = model.Detail;
                data.Price = model.Price;
                data.Image = model.Image;
                data.CodeName = model.CodeName;
                data.CategoryId = model.CategoryId;

                Context.Update(data);
                Context.SaveChanges();

                return NoContent();

            } 
            catch (Exception ex)
            {
                _logger.LogError($"Failed to execute PUT {ex.Message}");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Products data = Context.Products.SingleOrDefault(p => p.ProductId == id);
                if (data == null)
                {
                    return NotFound();
                }
                Context.Remove(data);
                Context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute DELETE");
                return BadRequest();
            }
        }
    }
}