using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RealtorDTOs
{
    public class RealtorCreateDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string? Password { get; set; }
        [Required]
        public int RealtorFirmId { get; set; }
        public string ProfilePicture { get; set; } = "https://placehold.co/600x400/png";
    }
}
