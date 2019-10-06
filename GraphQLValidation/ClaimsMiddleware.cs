﻿using System.Collections.Generic;
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
            // setting a fake role as an example
            var claims = new List<Claim>
            {
                new Claim("role", "NotAdmin")
            };

            // normally this would come in on the request, but i think its easier to run this example project if you dont also have to set up a contrived request to get it to work.
            httpContext.Request.Headers["x-user-id"] = "38DC888A-30C8-4180-90FA-068089D804AD";
            var appIdentity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(appIdentity);

            await _next(httpContext);
        }
    }
}