// RoomBookingUI/Models/RegisterViewModel.cs

using System.ComponentModel.DataAnnotations;

namespace RoomBookingUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = "User"; // "Admin" or "User"
    }
}
