using GraphQL;
using GraphQL.Execution;
using GraphQL.Validation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLValidation.GraphQl.DocumentListeners
{
    public class AttributeBasedAuthDocumentListener : DocumentExecutionListenerBase<GraphQLUserContext>
    {
        public override Task AfterValidationAsync(GraphQLUserContext userContext, IValidationResult validationResult, CancellationToken token)
        {
            if (userContext?.UserId != Guid.Empty)
            {
                if (userContext.RequestedProductIds?.Any() ?? false)
                {
                    // var isAllowed = httpClient.HeadAsync("userApi/<userid>/verify?productId=credit1,loan3...).IsSuccess()
                    var isAllowed = false;
                    if (!isAllowed)
                        validationResult.Errors.Add(
                            new ExecutionError(
                                $"Unable to verify ProductIds for user"));
                }
            }
            return Task.CompletedTask;
        }
    }
}
