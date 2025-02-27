using E_Commerce.DTOs.cartDTO;
using E_Commerce.DTOs.cartItemDTO;

namespace E_Commerce.Services.CartServices
{
    public interface ICartServices
    {
        Task<bool> AddItem(Guid usedId, Guid dto);
        Task<List<CartItemviewDto>> ViewCartItem(Guid id);

        Task<bool> DeleteItem(Guid userId, Guid uid);

        Task<CartViewDto> ViewCart(Guid id);

        Task<bool> Checkout(Guid userId, Guid address);


        Task<fullCart<CartItemviewDto>>  fullCart(Guid userId);

    }
}
