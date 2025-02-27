using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.AdressDTO
{
    public class AddressDto
    {

        [Required]
        public string? StreetAddress { get; set; }

        [Required]

        public string? City { get; set; }

        [Required]

        public string? State { get; set; }

        [Required]


        public int PostalCode { get; set; }

    }
}
