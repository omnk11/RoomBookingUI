// RoomBookingUI/Services/OrderService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApiService _apiService;

        public OrderService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string email)
        {
            return await _apiService.GetAsync<IEnumerable<OrderViewModel>>($"orders/user?email={email}");
        }

        public async Task<bool> BookRoomAsync(int roomId)
        {
            return await _apiService.PostAsync<int, bool>("orders/book", roomId);
        }

        public async Task<bool> CancelOrderAsync(int orderId)
        {
            return await _apiService.PostAsync<int, bool>("orders/cancel", orderId);
        }
    }
}
