// RoomBookingUI/Models/OrderViewModel.cs

using System;

namespace RoomBookingUI.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomLocation { get; set; }
        public decimal RoomPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
