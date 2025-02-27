using E_Commerce.DTOs.UserDTO;
using E_Commerce.DTOs.WhishListDto;

namespace E_Commerce.Services.WhishlistServices
{
    public interface IWhishlistServices
    {
        Task<bool> AddToWhishList(Guid userid, Guid productid);

        Task<List<WhishListDto>> ViewWhishList(Guid id);

        Task<bool> DeleteWhishList(Guid wid);
    }
}
