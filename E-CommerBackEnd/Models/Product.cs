using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required]

        public string ProductName { get; set; }


        [Required]

        public decimal Price { get; set; }

        [Required]

        public int stock { get; set; }

        [Required]

        public Guid CategoryId { get; set; }

        [Required]

        public string image { get; set; }

        public Category Category { get; set; }

        public ICollection<CartItem> cartItem { get; set; }

        public ICollection<Order> orders { get; set; }
    }
}
