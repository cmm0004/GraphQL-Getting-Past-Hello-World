using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GraphQLValidation
{
    public class GraphQLUserContext
    {
        public GraphQLUserContext(HttpRequest request)
        {
            //if (request.Headers.TryGetValue("x-user-id", out var userid))
            //{ 
            //  UserId = Guid.Parse(userid);
            //}
        }

        public Guid UserId { get; set; } = Guid.NewGuid();
        public HashSet<string> RequestedProductIds { get; set; } = new HashSet<string>();
    }
}