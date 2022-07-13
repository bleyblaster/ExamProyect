using AutoMapper;
using Domain.Model;
using ExamProyect.Model;
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
        private readonly IMapper _mapper;

        public ProductController(IProduct product, IMapper mapper)
        {
            _IProduct = product;
            _mapper = mapper;
        }
        [HttpGet(nameof(GetProductById))]
        public IActionResult GetProductById(int id)
        {
            var result = _IProduct.GetProduct(id);
            var ProductMap = _mapper.Map<ProductModel>(result);
            if (result is not null)
            {
                return Ok(ProductMap);
            }
            return BadRequest("No record Found");
        }
        [HttpGet(nameof(GetAllProduct))]
        public IActionResult GetAllProduct()
        {
            var result = _IProduct.GetAllProducts();
            var ProductMap = _mapper.Map<List<ProductModel>>(result);
            if (result.Count() > 0)
            {
                return Ok(ProductMap);
            }

            return BadRequest("No record loaded");
        }
        [HttpPost(nameof(InsertProduct))]
        public ActionResult InsertProduct(EditProductModel product)
        {

            var productMap = _mapper.Map<Product>(product);

            var result = _IProduct.InsertProduct(productMap);
            if (result)
            {
                return Ok("Created!");
            }

            return BadRequest("No Created");
        }

        [HttpPut(nameof(UpdateProduct))]
        public ActionResult UpdateProduct(ProductModel product)
        {
            var productMap = _mapper.Map<Product>(product);
            var result = _IProduct.UpdateProduct(productMap);
            if (result)
            {
                return Ok("Updated!");
            }

            return BadRequest("No Updated");
        }
        [HttpDelete(nameof(DeleteProduct))]
        public ActionResult DeleteProduct(ProductModel product)
        {
            var productMap = _mapper.Map<Product>(product);
            var result = _IProduct.DeleteProduct(productMap);
            if (result)
            {
                return Ok("Deleted!");
            }

            return BadRequest("No Deleted");
        }
    }
}
