// RoomBookingUI/Services/IOrderService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string email);
        Task<bool> BookRoomAsync(int roomId);
        Task<bool> CancelOrderAsync(int orderId);
    }
}
