using GraphQL;
using GraphQL.Authorization;
using GraphQL.Instrumentation;
using GraphQL.Types;
using System.Threading.Tasks;

namespace GraphQLValidation.GraphQl.FieldMiddleware
{
    public class AuthorizeMiddleware
    {
        public Task<object> Resolve(
            ResolveFieldContext context,
            FieldMiddlewareDelegate next)
        {
            if (context.FieldDefinition.RequiresAuthorization())
            {
                var err = new ExecutionError(
                    $"not authorized to access {context.FieldName}");
               
                context.Errors.Add(err);
                return Task.FromResult<object>(null);
            }
            else
            {
                return next(context);
            }
            
        }
    }
}
