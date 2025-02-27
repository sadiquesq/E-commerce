using E_Commerce.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }



        [Authorize(Roles ="User")]
        [HttpPost("PlaceOrder")]

        public async Task<IActionResult> PlaceOrder(Guid address,Guid pid,int Qut)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                var res = await _orderServices.placeorder(usedId, pid, Qut, address);
                if (!res)
                {
                    return NotFound("productid is not found");
                }
                return Ok("order placed successfully");
            }
            catch (Exception ex) 
            {
                return StatusCode(500,ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("ViewOrders")]


        public async Task<IActionResult> ViewOrders()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                var res =await  _orderServices.vieworders(usedId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [Authorize(Roles = "User")]
        [HttpDelete("DeleteOder{orderid}")]


        public async Task<IActionResult> DeleteOder(Guid orderid)
        {
            try
            {
                var res = await _orderServices.DeleteOrders(orderid);
                if(!res)
                {
                    return NotFound("Invalid OderId");
                }
                return Ok("successfully removed order");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }  

        }


        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateOder{orderid}")]

        public async Task<IActionResult> UpdateOder(Guid orderid,string orderstatus)
        {
            try
            {
                var res = await _orderServices.UpdateOrder(orderid, orderstatus);
                if (!res)
                {
                    return NotFound("Invalid OrderId");
                }
                return Ok($"order successfully updated into {orderstatus}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }







        [Authorize(Roles = "Admin")]
        [HttpGet("ViewAllOrders")]


        public async Task<IActionResult> ViewAllOrders()
        {
            try
            {
                var n = await _orderServices.viewallorders();
                return Ok(n);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet("TotalRevenue")]

        public async Task<IActionResult> TotalRevenue()
        {
            try
            {
                var res = await _orderServices.AllRevenus();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






    }
}
