using E_Commerce.DTOs.AdressDTO;
using E_Commerce.Services.AddressServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        [HttpPost("AddAddress")]

        public async Task<IActionResult> AddAddress([FromForm] AddressDto address)
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                _addressServices.AddAddress(usedId, address);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAllAddress")]

        public async Task<IActionResult> ViewAddress()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var ad=await _addressServices.ViewAddress(usedId);
                return Ok(ad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
