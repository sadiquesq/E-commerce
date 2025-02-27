using E_Commerce.DTOs.AdressDTO;

namespace E_Commerce.Services.AddressServices
{
    public interface IAddressServices
    {
        Task AddAddress(Guid uid,AddressDto addressDto);

        Task<List<AddressViewDio>> ViewAddress(Guid uid);
    }
}
