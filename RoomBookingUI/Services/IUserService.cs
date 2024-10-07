// RoomBookingUI/Services/IUserService.cs
using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public interface IUserService
    {
        Task<UserProfileViewModel> GetProfileAsync(string email);
    }
}
