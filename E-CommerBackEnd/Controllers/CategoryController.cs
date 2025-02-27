using E_Commerce.DTOs.CategoryDTO;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryServices _categoryservices;

        public CategoryController(ICategoryServices services)
        {
            _categoryservices = services;
        }

        public ICategoryServices ICategoryServices { get; }



        [Authorize(Roles ="Admin")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryInsertDto dto)
        {
            try
            {
                var n = await _categoryservices.AddCategory(dto);
                if (n)
                {
                    return Ok(dto);
                }
                else
                {
                    return StatusCode(400, "Category already exict");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("getAllCategory")]
        public async Task<IActionResult> getAllCategory()
        {
            try
            {
                var ve = await _categoryservices.Viewcategorys();
                return Ok(ve);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
