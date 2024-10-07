﻿// RoomBookingUI/Models/LoginResponse.cs

namespace RoomBookingUI.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
