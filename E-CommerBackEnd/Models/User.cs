using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class User
    {

        public Guid UserId { get; set; }


        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "invalid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        public bool IsBlock { get; set; } = true;

        public string Role { get; set; } = "User";

        
        public DateTime CreatedDate { get; set; }= DateTime.Now;


        public  Cart Cart { get; set; }

        public  ICollection<WhishList> WhishLists { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Address> addresses { get; set; }


    }
}
