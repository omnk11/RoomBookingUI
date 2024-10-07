// RoomBookingUI/Services/IRoomService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomViewModel>> GetAllRoomsAsync();
        Task<IEnumerable<RoomViewModel>> SearchRoomsAsync(string searchTerm);
        Task<RoomViewModel> GetRoomByIdAsync(int id);
        Task<bool> AddRoomAsync(RoomCreateViewModel model);
    }
}
