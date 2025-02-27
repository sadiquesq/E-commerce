using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs.WhishListDto;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services.WhishlistServices
{

  
    public class WhishlistServices : IWhishlistServices
    {

        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public WhishlistServices(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }



        public async Task<bool> AddToWhishList(Guid userid, Guid productid)
        {
            try
            {
                var n = await _mainDbContext.Products.FirstAsync(e => e.ProductId == productid);


                var m = _mainDbContext.WhishList.Where(e => e.ProductId == productid && e.UserId == userid);
                if (n == null && m != null)
                {
                    return false;
                }
                var whishlist = new WhishList()
                {
                    WhishlistId = Guid.NewGuid(),
                    ProductId = productid,
                    UserId = userid,
                };
                await _mainDbContext.WhishList.AddAsync(whishlist);
                await _mainDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<List<WhishListDto>> ViewWhishList(Guid id)
        {
            try
            {
                var n = await _mainDbContext.WhishList.Where(e => e.UserId == id).ToListAsync();
                return _mapper.Map<List<WhishListDto>>(n);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> DeleteWhishList(Guid wid)
        {
            try
            {
                var n = await _mainDbContext.WhishList.FirstOrDefaultAsync(e => e.WhishlistId == wid);
                if (n == null)
                {
                    return false;
                }
                _mainDbContext.WhishList.Remove(n);
                await _mainDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
