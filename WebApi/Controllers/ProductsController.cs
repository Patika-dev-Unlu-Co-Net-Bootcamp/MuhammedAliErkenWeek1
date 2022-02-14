using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

       
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(InMemoryDal.MemoryDal.ProductList);
        }

       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = CheckByIdIfItemExist(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NoContent();
        }

    
        
        [HttpPost()]
        public IActionResult Create([FromBody]Product product)
        {

            var result = CheckByIdIfItemExist(product.Id);
            if (result is not null)
            {
                return BadRequest();
            }
            
            try
            {
                InMemoryDal.MemoryDal.ProductList.Add(product);
            }
            catch (Exception)
            {

                return StatusCode(500); 
            }
            
            return Created("Index",new {message="Product added.", time = DateTime.Now });
            
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] Product product)
        {
            var result = CheckByIdIfItemExist(id);
            if (CheckByIdIfItemExist(product.Id) is  null)
            {
                return NotFound();
            }
           
            try
            {
                result.Id = product.Id != default ? product.Id : result.Id;
                result.ProductName = product.ProductName!= default ? product.ProductName:result.ProductName;
                result.CategoryId = product.CategoryId != default ? product.CategoryId : result.CategoryId; 
                result.PublishingDate = product.PublishingDate != default ? product.PublishingDate : result.PublishingDate;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            
            return Ok();

        }

       
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Product product)
        {

            var result = CheckByIdIfItemExist(product.Id);
         
            if (result is null)
            {
                return NotFound();
            }
            try
            {
                result.ProductName = product.ProductName;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = CheckByIdIfItemExist(id);

            if (result is null)
            {
                return BadRequest();
            }
            try
            {
                InMemoryDal.MemoryDal.ProductList.Remove(result);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
         

          
            return Ok();

        }

        [HttpHead("{id}")]
        public string PostHead(int id)
        {
            return "merhaba";
        }

        private Product CheckByIdIfItemExist(int id)
        {

            var temp = InMemoryDal.MemoryDal.ProductList.SingleOrDefault(p => p.Id == id);
            if (temp is not null)
            {
                return temp;
            }
            return null;

        }

        
        [HttpGet("/panel/{authorization}")]
        public IActionResult HowToReturn401(string authorization)
        {
            if (authorization=="user")
            {
                return Unauthorized();
            }
            return Ok();
        }

        
        [HttpGet("/panel/vip/{authorization}")]
        public IActionResult HowToReturn403(string authorization)
        {
            if (authorization == "user")
            {
                return StatusCode(403);
            }
            return Ok();
        }

        [HttpGet("/admin")]
        public IActionResult HowToReturn503()
        {
          
            return StatusCode(503);
        }

        [HttpGet("sortAscById")]
        public IActionResult SortAscById()
        {
            List<Product> temp;
            try
            {
                temp = InMemoryDal.MemoryDal.ProductList.OrderBy(p => p.Id).ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (temp.Count == 0)
            {
                return NotFound();
            }
            return Ok(temp);
        }
        [HttpGet("sortDescById")]
        public IActionResult SortDescById()
        {
            List<Product> temp;
            try
            {
                temp = InMemoryDal.MemoryDal.ProductList.OrderByDescending(p => p.Id).ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (temp.Count==0)
            {
                return NotFound();
            }
            return Ok(temp);
        }

    }
}
