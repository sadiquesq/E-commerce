using System.Text.Json.Serialization;

namespace E_Commerce.DTOs.cartItemDTO
{
    public class CartItemDto
    {

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]

        public decimal Amount { get; set; }
    }
}
