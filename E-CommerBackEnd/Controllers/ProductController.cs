using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductServices _productservices;

        public ProductController(IProductServices productservices)
        {
            _productservices = productservices;
        }




        [Authorize(Roles = "Admin")]
        [HttpPost("Addproducts")]

        public async Task<ActionResult> AddProducts( [FromForm]ProductDTO dTO, IFormFile image)
        {
            try
            {
                var p = await _productservices.AddProduct(dTO,image);
                if (!p)
                {
                    return BadRequest("product already exict");
                }
                return Ok(dTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("EditproductDetails")]
        public async Task<IActionResult> EditProduct(Guid id, [FromForm] productview1 dTO, IFormFile img)
        {
            try
            {
                var s =await _productservices.EditProduct(id, dTO,img);
                if(!s)
                {
                    return StatusCode(404, "product notfound");
                }
                return Ok(s);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    
        [Authorize]
        [HttpGet("viewproducts")]

        public async Task<IActionResult> ViewProducts()
        {
            try
            {
                var n = await _productservices.Viewproducts();
                return Ok(n);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("viewproductsBypagination")]

        public async Task<IActionResult> ViewProducts([FromBody] paginationParams paginationParams)
        {
            try
            {
                var n = await _productservices.viewProductByPagination(paginationParams);
                return Ok(n);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [Authorize]
        [HttpGet("GetProductById{id}")]

        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var n = await _productservices.Viewproductbyid(id);
                if (n == null)
                {
                    return NotFound("invalid id");
                }
                return Ok(n);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [Authorize]
        [HttpGet("ViewByCategory")]

        public async Task<IActionResult> ViewBycategory(string Categoryname)
        {
            try
            {
                var n = await _productservices.ViewByCategory(Categoryname);
                return Ok(n);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize]
        [HttpGet("SearchProduct{productname}")]

        public async Task<IActionResult>  SearchProduct(string productname)
        {
            try
            {
                var n =await  _productservices.SearchProducts(productname);
                if (n == null)
                {
                    return StatusCode(404, "there is no product on this name");
                }
                return Ok(n);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles ="Admin")]
        [HttpDelete("deleteproduct{id}")]

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var n = await _productservices.DeleteProduct(id);
                if (!n)
                {
                    return StatusCode(404, "there is no product on this id");
                }
                return Ok("successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
