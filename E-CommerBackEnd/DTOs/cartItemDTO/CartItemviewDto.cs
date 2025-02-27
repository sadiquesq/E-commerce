namespace E_Commerce.DTOs.cartItemDTO
{
    public class CartItemviewDto
    {

        public Guid CartItemId { get; set; }

        //public Guid ProductId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

    }
}
