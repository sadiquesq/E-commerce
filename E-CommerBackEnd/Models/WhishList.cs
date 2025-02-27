namespace E_Commerce.Models
{
    public class WhishList
    {

        public Guid WhishlistId { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public User User { get; set; }


    }
}
