using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs.UserDTO;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Services.AuthSerivces
{



    public class AuthService : IAuthService
    {

        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public AuthService(IMapper mapper, MainDbContext mainDbContext)
        {

            _mapper = mapper;
            _mainDbContext = mainDbContext;
        }


        public async Task<bool> CheckRegister(UserInsertDto dto)
        {
            try
            {
                var n = _mainDbContext.Users.FirstOrDefault(x => x.Email == dto.Email);
                if (n != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Rgister(UserInsertDto dto)
        {
            try
            {
                var haspassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                dto.Password = haspassword;
                var n = _mapper.Map<User>(dto);
                _mainDbContext.Users.Add(n);
                await _mainDbContext.SaveChangesAsync();

                int Total = 0;
                var user = _mainDbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
                var cart = new Cart()
                {
                    UserId = user.UserId,
                    TotalAmount = Total
                };
                _mainDbContext.carts.Add(cart);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<User> Login(LoginDto login)

        {
            try
            {
                var p = await _mainDbContext.Users.FirstOrDefaultAsync(e => login.Email == e.Email);
                if (p == null)
                {
                    return new User();
                }
                bool pass = BCrypt.Net.BCrypt.Verify(login.Password, p.Password);
                if (!pass)
                {
                    return new User();
                }
                return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

















    }
}
