// RoomBookingUI/Controllers/RoomsController.cs

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using RoomBookingUI.Models; // To recognize RoomViewModel
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomBookingUI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public RoomController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: /Rooms/Index?city=CityName
        public async Task<IActionResult> Index(string city)
        {
            var client = _clientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Construct the API URL based on whether a city is provided
            string apiUrl = string.IsNullOrEmpty(city)
                ? "https://localhost:5001/api/rooms"
                : $"https://localhost:5001/api/rooms/search?city={city}";

            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var rooms = JsonConvert.DeserializeObject<List<RoomViewModel>>(jsonString);
                return View(rooms);
            }

            // Optionally, add an error message to display in the view
            TempData["ErrorMessage"] = "Unable to fetch rooms. Please try again later.";
            return View(new List<RoomViewModel>());
        }
    }

    //    // GET: /Rooms/Add
    //    [Authorize(Roles = "Admin,Landlord")]
    //    public IActionResult Add()
    //    {
    //        return View();
    //    }

    //    // POST: /Rooms/Add
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    [Authorize(Roles = "Admin,Landlord")]
    //    public async Task<IActionResult> Add(RoomViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var client = _clientFactory.CreateClient();
    //            var token = HttpContext.Session.GetString("JWToken");

    //            if (!string.IsNullOrEmpty(token))
    //            {
    //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //            }

    //            var jsonContent = JsonConvert.SerializeObject(model);
    //            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

    //            var response = await client.PostAsync("https://localhost:5001/api/rooms", content);
    //            if (response.IsSuccessStatusCode)
    //            {
    //                return RedirectToAction("Index");
    //            }

    //            ModelState.AddModelError(string.Empty, "Failed to add room. Please try again.");
    //        }

    //        return View(model);
    //    }

    //    // Implement other actions like Edit, Delete as needed
    //}
}
