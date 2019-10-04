using System;
using System.Collections.Generic;
using System.Security.Claims;
using GraphQL.Authorization;
using Microsoft.AspNetCore.Http;

namespace GraphQLValidation
{
    /*
     * this class is defined by you to be whatever extra information you need to be included on your graphql requests
     * it needs to implement IProvideClaimsPrincipal for the out-of-the-box Authorization mechanism though.
     * you can set it on the request either by passing a function that returns this object (if using the graphqlmiddleware)
     * or creating it before calling execute on the document (as im doing in this example)
     */
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
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
        public HashSet<Guid> RequestedProductIds { get; set; } = new HashSet<Guid>();
    }
}