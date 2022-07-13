using AutoMapper;
using Repository.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProyect.Controllers.v1
{
    [Authorize]
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProduct _IProduct;

        public ProductController(IProduct product)
        {
            _IProduct = product;
        }
        [HttpGet(nameof(GetProductById))]
        public IActionResult GetProductById(int id)
        {
            var result = _IProduct.GetProduct(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No record Found");
        }
        [HttpGet(nameof(GetAllProduct))]
        public IActionResult GetAllProduct()
        {
            var result = _IProduct.GetAllProducts();
            if (result.Count() > 0)
            {
                return Ok(result);
            }

            return BadRequest("No record loaded");
        }
        [HttpPost(nameof(InsertProduct))]
        public ActionResult InsertProduct(EditProductModel product)
        {

            var result = _IProduct.InsertProduct(product);
            if (result)
            {
                return Ok("Created!");
            }

            return BadRequest("No Created");
        }

        [HttpPut(nameof(UpdateProduct))]
        public ActionResult UpdateProduct(ProductModel product)
        {
            var result = _IProduct.UpdateProduct(product);
            if (result)
            {
                return Ok("Updated!");
            }

            return BadRequest("No Updated");
        }
        [HttpDelete(nameof(DeleteProduct))]
        public ActionResult DeleteProduct(ProductModel product)
        {
            var result = _IProduct.DeleteProduct(product);
            if (result)
            {
                return Ok("Deleted!");
            }

            return BadRequest("No Deleted");
        }
    }
}
