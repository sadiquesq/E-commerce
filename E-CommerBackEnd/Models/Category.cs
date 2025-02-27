namespace E_Commerce.Models
{
    public class Category
    {

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}
