using System;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLValidation.GraphQl.DocumentListeners
{
    public class AttributeBasedAuthDocumentListener : DocumentExecutionListenerBase<GraphQLUserContext>
    {
        public override Task AfterValidationAsync(GraphQLUserContext userContext, IValidationResult validationResult, 
            CancellationToken token)
        {
            if (userContext.UserId != default)
            {
                if (userContext.RequestedProductIds?.Any() ?? false)
                {
                    // have your api call return the ids that *were* valid
                    Thread.Sleep(300);

                    //use the user context to pass those back down to your field resolver
                    // maybe only the first one was valid
                    // you could log the invalid ones or something, or set it on the userContext
                    // and create an error later to return to the caller.
                    userContext.RequestedProductIds = new HashSet<string>() { userContext.RequestedProductIds.First() };
                }
            }
            return Task.CompletedTask;
        }
    }
}
