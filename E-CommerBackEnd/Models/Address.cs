namespace E_Commerce.Models
{
    public class Address
    {

        public Guid AddressId { get; set; }

        public Guid UserId { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }
        public string State { get; set; }
   
        public int PostalCode { get; set; }

        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
