// RoomBookingUI/Services/UserService.cs
using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<UserProfileViewModel> GetProfileAsync(string email)
        {
            return await _apiService.GetAsync<UserProfileViewModel>($"users/profile?email={email}");
        }
    }
}
