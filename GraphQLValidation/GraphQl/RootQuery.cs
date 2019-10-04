using GraphQL.Types;

namespace GraphQLValidation.GraphQl
{

    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Name = "Query";

            Field<ProductsQuery>("products", resolve: _ => new { });
        }
    }
}
