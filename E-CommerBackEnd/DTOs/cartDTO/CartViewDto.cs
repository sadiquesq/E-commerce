using E_Commerce.Models;

namespace E_Commerce.DTOs.cartDTO
{
    public class CartViewDto
    {

        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public int ProductCount { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
