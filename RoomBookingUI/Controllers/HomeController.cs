using Microsoft.AspNetCore.Mvc;
using RoomBookingUI.Models;
using System.Diagnostics;
// RoomBookingUI/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;


namespace RoomBookingUI.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("JWToken") != null)
            {
                ViewBag.Email = GetUserEmail();
            }
            return View();
        }

        private string GetUserEmail()
        {
            // Optionally, decode JWT to get user email
            return "user@example.com";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Additional actions
    }

}
