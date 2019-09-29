using System.Diagnostics.CodeAnalysis;
using GraphQL.Types;
using GraphQLValidation.GraphQl.Types;

namespace GraphQLValidation.GraphQl
{
    [ExcludeFromCodeCoverage]
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Name = "Query";
            Field<ListGraphType<UserProductQuery>>("userProduct", resolve: _ => Data.Data.GetUserProducts());
        }
    }
}