using E_Commerce.DTOs.UserDTO;
using E_Commerce.Services.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Data;
using System.Numerics;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService _AdminService;

        public AdminController(IAdminService adminService)
        {
            _AdminService = adminService;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getAllUsers")]

        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var us = await _AdminService.GetAllUsers();
                return Ok(us);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getUserById{id}" )]


        public async Task<IActionResult>  GetUserById(Guid id)
        {
            try
            {
                var us = await _AdminService.GetUserById(id);
                if (us.UserName == null)
                {
                    return BadRequest("invalid id");
                }
                return Ok(us);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [Authorize(Roles ="Admin")]
        [HttpPatch("BlockOrUnblock{id}")]

        public async Task<IActionResult> BlockOrUnblock(Guid id)
        {
            try
            {
                var us = await _AdminService.BlockOrUnblock(id);
                return StatusCode(us.StatusCode, us.Message);
            }
             catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



       
    }
}
