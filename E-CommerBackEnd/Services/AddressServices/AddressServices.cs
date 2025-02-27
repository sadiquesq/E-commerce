using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs.AdressDTO;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Mapper;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace E_Commerce.Services.AddressServices
{
    public class AddressServices : IAddressServices
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public AddressServices(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }

        public async Task AddAddress(Guid uid,AddressDto addressDto)
        {
            try
            {
                var n = _mapper.Map<Address>(addressDto);
                n.UserId = uid;

                await _mainDbContext.Address.AddAsync(n);
                await _mainDbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AddressViewDio>> ViewAddress(Guid uid)
        {
            try
            {
                var address = await _mainDbContext.Address.Where(e => e.UserId == uid).ToListAsync();
                return _mapper.Map<List<AddressViewDio>>(address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
