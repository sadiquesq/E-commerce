using E_Commerce.Services.WhishlistServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhishListController : ControllerBase
    {

        private readonly IWhishlistServices _whishlistService;

        public WhishListController(IWhishlistServices whishlistService)
        {
            _whishlistService = whishlistService;
        }


        [Authorize(Roles = "User")]
        [HttpPost("AddToWhishlist")]

        public async Task<IActionResult> AddToWhishList(Guid productid)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                var res = await _whishlistService.AddToWhishList(usedId, productid);

                if (!res)
                {
                    return NotFound("Invalid ProductId");
                }
                return Ok("successfully added to whishlist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles ="User")]
        [HttpGet("ViewWhishList")]

        public async Task<IActionResult>  ViewWhishList()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                var res = await _whishlistService.ViewWhishList(usedId);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = "User")]
        [HttpDelete("DeleteWhishList")]

        public async Task<IActionResult> DeleteWhishList(Guid Wid)
        {
            try
            {
                var res = await _whishlistService.DeleteWhishList(Wid);
                if(!res)
                {
                    return NotFound("Invalid whishlistId");
                }
                return Ok("successfully removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
    }
