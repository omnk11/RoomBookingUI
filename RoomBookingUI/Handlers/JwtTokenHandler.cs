
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RoomBookingUI.Handlers


{
    // RoomBookingUI/Handlers/JwtTokenHandler.cs


    namespace RoomBookingUI.Handlers
    {
        public class JwtTokenHandler : DelegatingHandler
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // Retrieve the JWT token from the cookies
                var token = _httpContextAccessor.HttpContext.Request.Cookies["JWTToken"];

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                return base.SendAsync(request, cancellationToken);
            }
        }
    }

}
