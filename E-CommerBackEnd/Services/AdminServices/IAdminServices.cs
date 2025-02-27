using E_Commerce.DTOs;
using E_Commerce.DTOs.UserDTO;

namespace E_Commerce.Services.AdminServices
{
    public interface IAdminService
    {
        Task<ApiRespones<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(Guid id);

        Task<Result> BlockOrUnblock(Guid Id);


    }
}
