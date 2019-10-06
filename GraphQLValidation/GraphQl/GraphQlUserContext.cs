using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GraphQLValidation
{
    /*
     * this class is defined by you to be whatever extra information you need to be included on your graphql requests
     * it needs to implement IProvideClaimsPrincipal for the out-of-the-box Authorization mechanism though.
     * you can set it on the request either by passing a function that returns this object (if using the graphqlmiddleware)
     * or creating it before calling execute on the document (as im doing in this example)
     */
    public class GraphQLUserContext
    {
        public GraphQLUserContext(HttpRequest request)
        {
            if (request.Headers.TryGetValue("x-user-id", out var userid))
            { 
              UserId = Guid.Parse(userid);
            }
        }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public HashSet<string> RequestedProductIds { get; set; } = new HashSet<string>();
    }
}