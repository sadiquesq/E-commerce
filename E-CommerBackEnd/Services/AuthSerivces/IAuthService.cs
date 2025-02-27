using E_Commerce.DTOs.UserDTO;
using E_Commerce.Models;

namespace E_Commerce.Services.AuthSerivces
{
    public interface IAuthService
    {
        Task<bool> CheckRegister(UserInsertDto dto);
        Task<bool> Rgister(UserInsertDto dto);


        Task<User> Login(LoginDto login);
    }
}
