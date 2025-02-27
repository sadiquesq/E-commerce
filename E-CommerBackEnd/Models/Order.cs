using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Order
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]

        public Guid UserId { get; set; }

        [Required]

        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]

        public Guid AddressId { get; set; }

        public DateTime Orderdate { get; set; }= DateTime.Now;

        public string OrderStatus { get; set; } = "shipping";

        public  decimal TotalAmount { get; set; }


        public User User { get; set; }

        public Product Product { get; set; }

        public Address Address { get; set; }
    }
}
