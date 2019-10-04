using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GraphQLValidation
{
    public class ClaimsMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                new Claim("role", "NotAnAdmin")
            };

            httpContext.Request.Headers["x-user-id"] = "38DC888A-90C8-4180-90FA-068089D504AD";
            var appIdentity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(appIdentity);

            await _next(httpContext);
        }
    }
}