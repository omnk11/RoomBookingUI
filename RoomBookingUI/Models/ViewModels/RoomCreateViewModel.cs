// RoomBookingUI/Models/RoomCreateViewModel.cs

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingUI.Models
{
    public class RoomCreateViewModel
    {
        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Room Image")]
        public IFormFile Image { get; set; }

        [Required]
        public string OwnerEmail { get; set; }
    }
}
