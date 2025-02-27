using E_Commerce.DTOs.cartItemDTO;
using E_Commerce.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartContoller : ControllerBase
    {

        private readonly ICartServices _cartServices;

        public CartContoller(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [Authorize(Roles = "User")]
        [HttpGet("ViewCart")]


        public async Task<IActionResult> ViewCart()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var res =await _cartServices.ViewCart(usedId);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [Authorize(Roles = "User")]
        [HttpPost("AddToCart")]

        public async Task<IActionResult> AddTocart(Guid dto)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var res = await _cartServices.AddItem(usedId, dto);
                if (!res)
                {
                    return NotFound();
                }

                return Ok("added succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = "User")]
        [HttpGet("ViewCartItems")]


        public async Task<IActionResult> ViewCartItems()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var res = await _cartServices.ViewCartItem(usedId);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [Authorize(Roles = "User")]
        [HttpPost("CheckOut")]


        public async Task<IActionResult> CheckOut(Guid address)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var res = await _cartServices.Checkout(usedId, address);
                if (!res)
                {
                    return StatusCode(204, "there is no product incart");
                }
                return Ok("successfully ordered all cartItems");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }





            [Authorize(Roles = "User")]
        [HttpDelete("DeleteCartItems")]


        public async Task<IActionResult> DeleteCartItems(Guid CartItemid)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                var n = await _cartServices.DeleteItem(usedId, CartItemid);
                if (!n)
                {
                    return BadRequest("invalid cartitemid");
                }
                return Ok("successfully removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = "User")]
        [HttpGet("ViewFullCart")]


        public async Task<IActionResult> ViewFullCart()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var res = await _cartServices.fullCart(usedId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
