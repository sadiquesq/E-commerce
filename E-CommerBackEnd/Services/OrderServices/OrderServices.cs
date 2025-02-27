using AutoMapper;
using E_Commerce.Controllers;
using E_Commerce.DTOs;
using E_Commerce.DTOs.OderDTO;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace E_Commerce.Services.OrderServices
{

  
    public class OrderServices : IOrderServices
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public OrderServices(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }




        public async Task<bool> placeorder(Guid uid, Guid pid, int qut, Guid ad)
        {
            try
            {
                var prod = await _mainDbContext.Products.FirstOrDefaultAsync(e => e.ProductId == pid);
                if (prod == null)
                {
                    return false;
                }
                var order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    ProductId = pid,
                    UserId = uid,
                    Quantity = qut,
                    AddressId = ad,
                    Orderdate = DateTime.Now,
                    OrderStatus = "order placed",
                    TotalAmount = prod.Price * qut
                };
                _mainDbContext.Orders.Add(order);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<List<OrderViewDTO>> vieworders(Guid uid)
        {
            try
            {
                var n = await _mainDbContext.Orders.Where(e => e.UserId == uid).Include(e => e.Product).ToListAsync();
                var p = _mapper.Map<List<OrderViewDTO>>(n);
                return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<bool> DeleteOrders(Guid Oid)
        {
            try
            {
                var n = await _mainDbContext.Orders.FirstOrDefaultAsync(e => e.OrderId == Oid);
                if (n == null)
                {
                    return false;
                }
                _mainDbContext.Orders.Remove(n);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> UpdateOrder(Guid oid, string status)
        {
            try
            {
                var n = await _mainDbContext.Orders.FirstOrDefaultAsync(e => e.OrderId == oid);
                if (n == null)
                {
                    return false;
                }
                n.OrderStatus = status;
                _mainDbContext.Orders.Update(n);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<List<OrderAdminViewdto>> viewallorders()
        {
            try
            {
                var n = await _mainDbContext.Orders.Include(e => e.Product).ToListAsync();
                var p = _mapper.Map<List<OrderAdminViewdto>>(n);
                return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public async Task<revenuesDto> AllRevenus()
        {
            try
            {
                var n = await _mainDbContext.Orders.Where(e => e.OrderStatus == "Delived").ToListAsync();

                return new revenuesDto
                {
                    ProductCount = n.Count,
                    revenues = n.Sum(e => e.TotalAmount)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }

}
