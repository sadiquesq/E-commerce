using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Cart
    {
        [Required]
        public Guid CartId { get; set; }

        [Required]

        public Guid UserId { get; set; }

        [Required]

        public int ProductCount { get; set; }

        [Required]

        public decimal TotalAmount { get; set; }


        public ICollection<CartItem> CartItems { get; set; }

        public User User { get; set; }
    }
}
