using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.UserDTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }


        public string Email { get; set; }

        public string Role { get; set; }

        public bool Isblock { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
