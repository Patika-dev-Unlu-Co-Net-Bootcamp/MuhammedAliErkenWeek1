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
    public class ProductDetailsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(InMemoryDal.MemoryDal.ProductDetailList);
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
        public IActionResult Create([FromBody] ProductDetail productDetail)
        {
            var result = CheckByIdIfItemExist(productDetail.Id);
            if (result is not null)
            {
                return BadRequest();
            }

            try
            {
                InMemoryDal.MemoryDal.ProductDetailList.Add(productDetail);
            }
            catch (Exception)
            {

                return StatusCode(500); 
            }
            return Created("Index", new { message = "ProductDetails added.", time = DateTime.Now });

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDetail productDetail)
        {
            var result = CheckByIdIfItemExist(productDetail.Id);
            if (CheckByIdIfItemExist(productDetail.Id) is null)
            {
                return NotFound();
            }

            try
            {
                result.UnitInStock = productDetail.UnitInStock;
                result.UnitOnOrder = productDetail.UnitOnOrder;
                result.UnitPrice = productDetail.UnitPrice;
                result.Description = productDetail.Description;
                result.QuantityPerUnit = productDetail.QuantityPerUnit;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

            return Ok();
            

        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] ProductDetail productDetail)
        {
            var result = CheckByIdIfItemExist(productDetail.Id);
            if (CheckByIdIfItemExist(productDetail.Id) is null)
            {
                return NotFound();
            }

            try
            {
                result.UnitPrice = productDetail.UnitPrice;
           
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
                InMemoryDal.MemoryDal.ProductDetailList.Remove(result);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            return Ok();

        }

        private ProductDetail CheckByIdIfItemExist(int id)
        {

            var temp = InMemoryDal.MemoryDal.ProductDetailList.SingleOrDefault(p => p.Id == id);
            if (temp is not null)
            {
                return temp;
            }
            return null;

        }


    }
}
