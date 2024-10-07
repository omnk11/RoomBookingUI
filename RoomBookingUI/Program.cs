// RoomBookingUI/Program.cs

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http.Headers;
using RoomBookingUI.Services;
using MicrosoftAuthenticationService = Microsoft.AspNetCore.Authentication.AuthenticationService;
using RoomBookingAuthenticationService = RoomBookingUI.Services.AuthenticationService;

using Microsoft.AspNetCore.Authentication;
using RoomBookingUI.Handlers.RoomBookingUI.Handlers;

var builder = WebApplication.CreateBuilder(args);

// =========================
// 1. Configure Services
// =========================

// 1.1 Add Controllers with Views
builder.Services.AddControllersWithViews();

// 1.2 Configure Authentication with Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to Login if not authenticated
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to Access Denied page
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

// 1.3 Configure Session (Optional)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 1.4 Configure HttpClient for API Communication
builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
})
.AddHttpMessageHandler<RoomBookingUI.Handlers.RoomBookingUI.Handlers.JwtTokenHandler>();

// Custom DelegatingHandler to add JWT tokens
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// 1.5 Register Custom Services for Dependency Injection
builder.Services.AddScoped<RoomBookingUI.Services.IAuthenticationService, RoomBookingAuthenticationService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// 1.6 Add Singleton for JWT Token Handler
builder.Services.AddTransient<JwtTokenHandler>();

// 1.7 Configure Razor Pages (if needed)
builder.Services.AddRazorPages();

// 1.8 Configure CORS (Optional, if needed)



// 1.9 Add Logging (Optional but recommended)
builder.Services.AddLogging();

// =========================
// 2. Build the Application
// =========================
var app = builder.Build();

// =========================
// 3. Configure Middleware Pipeline
// =========================

// 3.1 Configure Exception Handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Redirect to Error page
    app.UseHsts(); // Use HTTP Strict Transport Security
}
else
{
    app.UseDeveloperExceptionPage(); // Detailed error pages in development
}

// 3.2 Use HTTPS Redirection
app.UseHttpsRedirection();

// 3.3 Serve Static Files (wwwroot)
app.UseStaticFiles();

// 3.4 Enable Routing
app.UseRouting();

// 3.5 Enable CORS (Optional)

// 3.6 Enable Session (Optional)
app.UseSession();

// 3.7 Enable Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// 3.8 Configure Endpoint Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 3.9 Map Razor Pages (if used)
app.MapRazorPages();

// =========================
// 4. Run the Application
// =========================
app.Run();
