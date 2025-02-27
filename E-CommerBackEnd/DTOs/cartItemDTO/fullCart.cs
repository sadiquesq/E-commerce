namespace E_Commerce.DTOs.cartItemDTO
{
    public class fullCart<T>
    {
        public List<T> Items { get; set; }

        public int TotalItems { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
