using System.Collections.Generic;
using System.Security.Claims;
using GraphQL.Authorization;
using Microsoft.AspNetCore.Http;

namespace GraphQLValidation
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public GraphQLUserContext() { }
        public GraphQLUserContext(HttpRequest request)
        {
            if (request.Headers.TryGetValue("x-user-id", out var userid))
            { 
              UserId = userid;
            }

            User = request.HttpContext.User;
        }
        public ClaimsPrincipal User { get; set; }
        public string UserId { get; set; }
        public HashSet<string> RequestedProductIds { get; set; } = new HashSet<string>();
    }
}