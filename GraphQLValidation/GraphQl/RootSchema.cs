using System.Diagnostics.CodeAnalysis;
using GraphQL;
using GraphQL.Types;

namespace GraphQLValidation.GraphQl
{
    [ExcludeFromCodeCoverage]
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
        }
    }
}
