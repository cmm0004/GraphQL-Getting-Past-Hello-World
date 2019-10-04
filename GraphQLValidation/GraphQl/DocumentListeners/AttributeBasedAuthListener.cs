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
    public class AttributeBasedAuthListener : IDocumentExecutionListener
    {
        public Task AfterValidationAsync(object userContext, IValidationResult validationResult, CancellationToken token)
        {
            //// pretend we are making an external api call here to determine whether the user owns these productIds
            //// in our use case, we use a HEAD call to minimize effort, just "200 or 403, are all of these ids valid for this user?" HEAD: userapi/<userid>/verify?productid=1,2....
            //// because we add a validation error, it causes the entire call to be cancelled immediately after this hook.
            //var context = (GraphQLUserContext)userContext;

            //if (!string.IsNullOrWhiteSpace(context?.UserId))
            //{
            //    if (context.RequestedProductIds?.Any() ?? false)
            //    {
            //         Thread.Sleep(200);

            //        var wasValid = true;
            //        // var wasValid = false;

            //        if (!wasValid)
            //        {
            //           validationResult.Errors.Add(
            //               new ExecutionError($"Unable to verify ProductIds for user: {string.Join(", ", context.RequestedProductIds)} for user: {context.UserId} : {"One or more wasnt authorized"}"));
            //        }
                   
            //    }
            //}

            
            /*
             * alternatively
             */
            // what if you still wanted to resolve as much as you could?
            // youd need to have your api call return the ids that *were* valid, and use the user context to pass those back down to 
            var context = (GraphQLUserContext)userContext;

            if (!string.IsNullOrWhiteSpace(context?.UserId))
            {
                if (context.RequestedProductIds?.Any() ?? false)
                {
                    Thread.Sleep(200);

                    var wasValid = true;
                    // var wasValid = false;

                    if (!wasValid)
                    {
                        validationResult.Errors.Add(
                            new ExecutionError($"Unable to verify ProductIds for user: {string.Join(", ", context.RequestedProductIds)} for user: {context.UserId} : {"One or more wasnt authorized"}"));
                    }

                }
            }

            return Task.CompletedTask;
        }

        public Task BeforeExecutionAsync(object userContext, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task BeforeExecutionAwaitedAsync(object userContext, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task AfterExecutionAsync(object userContext, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task BeforeExecutionStepAwaitedAsync(object userContext, CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
