using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.OderDTO
{
    public class OrderAdminViewdto
    {


        [Required]
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]

        public string Address { get; set; }

        public DateTime Orderdate { get; set; } = DateTime.Now;

        public string OrderStatus { get; set; } = "shipping";

        public decimal TotalAmount { get; set; }


    }
}
