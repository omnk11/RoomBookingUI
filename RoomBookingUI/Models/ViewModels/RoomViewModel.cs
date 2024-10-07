// RoomBookingUI/Models/RoomViewModel.cs

namespace RoomBookingUI.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string OwnerEmail { get; set; }
        // Add other room-related fields as needed
    }
}
