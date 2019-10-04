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
        public override Task AfterValidationAsync(GraphQLUserContext userContext, IValidationResult validationResult, CancellationToken token)
        {
            /*
             * pretend we are making an external api call here to determine whether the user owns these productIds
             * in Kabbage's use case, we use a HEAD call to minimize effort, just "200 or 403, are all of these ids valid for this user?" HEAD: userapi/<userid>/verify?productid=1,2....
             * because we add a validation error, it causes the entire call to be cancelled immediately after this hook.
             */

            if (!string.IsNullOrWhiteSpace(userContext?.UserId))
            {
                if (userContext.RequestedProductIds?.Any() ?? false)
                {
                    Thread.Sleep(200);

                    var wasValid = true;
                    // var wasValid = false;

                    if (!wasValid)
                    {
                        validationResult.Errors.Add(
                            new ExecutionError($"Unable to verify ProductIds for user: {string.Join(", ", userContext.RequestedProductIds)} for user: {userContext.UserId} : {"One or more wasnt authorized"}"));
                    }

                }
            }

            return Task.CompletedTask;


            /*
             * alternatively
             * what if you still wanted to resolve as much as you could?
             * youd need to have your api call return the ids that *were* valid, and use the user context to pass those back down to 
             */
            //if (!string.IsNullOrWhiteSpace(userContext?.UserId))
            //{
            //    if (userContext.RequestedProductIds?.Any() ?? false)
            //    {
            //        Thread.Sleep(200);

            //        // maybe only the first one was valid
            //        // you could log the invalid ones or something, or set it on the userContext and create an error later to return to the caller.
            //        userContext.RequestedProductIds = new HashSet<Guid>(){userContext.RequestedProductIds.First()};
            //    }
            //}

            //return Task.CompletedTask;
        }
    }
}
