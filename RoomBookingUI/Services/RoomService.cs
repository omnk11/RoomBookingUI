// RoomBookingUI/Services/RoomService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public class RoomService : IRoomService
    {
        private readonly IApiService _apiService;

        public RoomService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<RoomViewModel>> GetAllRoomsAsync()
        {
            return await _apiService.GetAsync<IEnumerable<RoomViewModel>>("rooms");
        }

        public async Task<IEnumerable<RoomViewModel>> SearchRoomsAsync(string searchTerm)
        {
            return await _apiService.GetAsync<IEnumerable<RoomViewModel>>($"rooms/search?query={searchTerm}");
        }

        public async Task<RoomViewModel> GetRoomByIdAsync(int id)
        {
            return await _apiService.GetAsync<RoomViewModel>($"rooms/{id}");
        }

        public async Task<bool> AddRoomAsync(RoomCreateViewModel model)
        {
            return await _apiService.PostAsync<RoomCreateViewModel, bool>("rooms/add", model);
        }
    }
}
