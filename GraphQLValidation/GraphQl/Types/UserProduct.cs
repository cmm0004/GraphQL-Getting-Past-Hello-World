using GraphQL.Authorization;
using GraphQL.Types;

namespace GraphQLValidation.GraphQl.Types
{
    public class UserProductQuery : ObjectGraphType<UserProduct>
    {
        public UserProductQuery()
        {
            Name = "UserProduct";
            //this.AuthorizeWith("AdminPolicy");
            Field(u => u.UserProductId, type: typeof(IdGraphType));
            Field(u => u.IsGoodStanding);
            Field(u => u.UserId);

            Field<ListGraphType<LoanQuery>>(
                "Loans",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "id", Description = "loan id"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    return Data.Data.GetLoansForUser(context.Source.UserProductId, id);
                }
            );

            Field<ListGraphType<CashAdvanceQuery>>(
                "CashAdvances",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id", Description = "cash advance id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    return Data.Data.GetCashAdvancesForUser(context.Source.UserProductId, id);
                }
            ).AuthorizeWith("AdminPolicy");
        }
    }
}

