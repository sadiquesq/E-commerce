using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs.cartDTO;
using E_Commerce.DTOs.cartItemDTO;
using E_Commerce.Models;
using E_Commerce.Services.OrderServices;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services.CartServices
{

    public class CartServices : ICartServices
    {

        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;

        public CartServices(MainDbContext mainDbContext, IMapper mapper, IOrderServices orderServices)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _orderServices = orderServices;
        }



        public async Task<CartViewDto> ViewCart(Guid id)
        {
            try
            {
                var cart = await _mainDbContext.carts.FirstOrDefaultAsync(e => e.UserId == id);
                var n = _mapper.Map<CartViewDto>(cart);
                return n;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }




        public async Task<bool> AddItem(Guid userId, Guid item)
        {

            try
            {
                var cart = await _mainDbContext.carts.FirstOrDefaultAsync(e => e.UserId == userId);

                var x = _mainDbContext.Products.FirstOrDefault(e => e.ProductId == item);
                var res = _mainDbContext.CartItems.FirstOrDefault(e => e.ProductId == item && e.CartId == cart.CartId);

                if (res != null)
                {
                    res.Quantity = res.Quantity + 1;
                    _mainDbContext.CartItems.Update(res);
                    await _mainDbContext.SaveChangesAsync();
                }
                else
                {
                    var n = _mapper.Map<CartItem>(item);
                    n.Quantity +=n.Quantity+1;
                    n.CartId = cart.CartId;
                    n.Amount = x.Price * n.Quantity;
                    await _mainDbContext.CartItems.AddAsync(n);
                    await _mainDbContext.SaveChangesAsync();

                }

                cart.TotalAmount = _mainDbContext.CartItems.Where(n => n.CartId == cart.CartId).Sum(n => n.product.Price * n.Quantity);
                cart.ProductCount = _mainDbContext.CartItems.Where(n => n.CartId == cart.CartId).Count();

                await _mainDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }


        public async Task<List<CartItemviewDto>> ViewCartItem(Guid id)
        {
            try
            {
                var cart = await _mainDbContext.carts.FirstOrDefaultAsync(e => e.UserId == id);
                var cartitems = await _mainDbContext.CartItems.Where(e => e.CartId == cart.CartId).Include(e => e.product).ToListAsync();
                var m = _mapper.Map<List<CartItemviewDto>>(cartitems);

                return m;
            }

            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.InnerException?.Message ?? ex.Message}");

            }


        }


        public async Task<bool> DeleteItem(Guid userId, Guid Gid)
        {
            try
            {
                var cart = await _mainDbContext.carts.FirstOrDefaultAsync(e => e.UserId == userId);
                var res = await _mainDbContext.CartItems.FirstOrDefaultAsync(e => e.CartItemId == Gid);
                if (res == null)
                {
                    return false;
                }
                _mainDbContext.CartItems.Remove(res);
                await _mainDbContext.SaveChangesAsync();


                cart.TotalAmount = _mainDbContext.CartItems.Where(n => n.CartId == cart.CartId).Sum(n => n.product.Price * res.Quantity);
                cart.ProductCount = _mainDbContext.CartItems.Where(n => n.CartId == cart.CartId).Count();
                await _mainDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<bool> Checkout(Guid userId, Guid address)
        {
            try
            {
                var cart = await _mainDbContext.carts.FirstOrDefaultAsync(e => e.UserId == userId);

                var cartitmes = await _mainDbContext.CartItems.Where(e => e.CartId == cart.CartId).ToListAsync();
                if (!cartitmes.Any())
                {
                    return false;
                }
                foreach (var item in cartitmes)
                {
                    var result = await _orderServices.placeorder(userId, item.ProductId, item.Quantity, address);
                    _mainDbContext.CartItems.Remove(item);
                }
                await _mainDbContext.SaveChangesAsync();


                cart.TotalAmount = 0;
                cart.ProductCount = 0;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }


        public async Task<fullCart<CartItemviewDto>> fullCart(Guid userId)
        {
            var cart = await _mainDbContext.carts.FirstOrDefaultAsync(e => e.UserId == userId);
            var cartitems = await _mainDbContext.CartItems.Where(e => e.CartId == cart.CartId).Include(e => e.product).ToListAsync();
            var m = _mapper.Map<List<CartItemviewDto>>(cartitems);
            return new fullCart<CartItemviewDto> 
            { 
                Items=m,
                TotalItems=cart.ProductCount,
                TotalAmount=cart.TotalAmount
              };

        }








    }
}
