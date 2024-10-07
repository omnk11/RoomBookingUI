// RoomBookingUI/Services/AuthenticationService.cs
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RoomBookingUI.Models;

namespace RoomBookingUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;


       


        public AuthenticationService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var response = await _apiService.PostAsync<LoginViewModel, LoginResponse>("auth/login", model);
            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                // Store the JWT token in a cookie
                _httpContextAccessor.HttpContext.Response.Cookies.Append("JWTToken", response.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = System.DateTime.UtcNow.AddMinutes(60)
                });

                // Create user claims and sign in with cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.Email),
                    new Claim(ClaimTypes.Role, response.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(60)
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            // Remove the JWT token cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("JWTToken");

            // Sign out of the cookie authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var response = await _apiService.PostAsync<RegisterViewModel, RegisterResponse>("auth/register", model);
            return response != null && response.Success;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
