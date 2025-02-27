using E_Commerce.DTOs.UserDTO;
using E_Commerce.Models;
using E_Commerce.Services.AuthSerivces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;



        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }


        [HttpPost("SignUp")]

        public async Task<IActionResult> SignUp(UserInsertDto user)
        {
            try
            {
                var n = await _authService.CheckRegister(user);
                if (!n)
                {
                    return BadRequest("user already exist");
                }

                var nv = await _authService.Rgister(user);
                return Ok("sucessfully registered");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server eroor" + ex);

            }
        }


        [HttpPost("login")]

        public async Task<IActionResult?> Login(LoginDto login)
        {
            try
            {

                var x =await  _authService.Login(login);
                if (!x.IsBlock)
                {
                    return StatusCode(401, "user is block");
                }

                if (x.UserName == null)
                {
                    return BadRequest("invalid email or password");
                }
               
                string token = CreateToken(x);
                return Ok(token);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server eroor" + ex);

            }
        }





        private string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim (ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
