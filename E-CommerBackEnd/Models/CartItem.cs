namespace E_Commerce.Models
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Product product { get; set; }
    }
}
