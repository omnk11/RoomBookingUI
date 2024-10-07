// RoomBookingUI/Services/IAuthenticationService.cs

using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> RegisterAsync(RegisterViewModel model);
        bool IsAuthenticated();
        string GetUserEmail();
    }
}
