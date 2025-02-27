using E_Commerce.DTOs.OderDTO;
using E_Commerce.DTOs;

namespace E_Commerce.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<bool> placeorder(Guid uid, Guid oid, int qut, Guid ad);
        Task<List<OrderViewDTO>> vieworders(Guid uid);

        Task<bool> DeleteOrders(Guid Oid);

        Task<bool> UpdateOrder(Guid oid, string status);

        Task<List<OrderAdminViewdto>> viewallorders();

        Task<revenuesDto> AllRevenus();

    }
}
