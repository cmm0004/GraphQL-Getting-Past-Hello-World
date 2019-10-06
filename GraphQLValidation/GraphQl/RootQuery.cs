using GraphQL.Types;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl.Models;
using GraphQLValidation.GraphQl.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraphQLValidation.GraphQl
{

    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IHttpContextAccessor accessor)
        {
            Name = "Query";
            Field<CreditLineType>()
                .Name("CreditLine")
                .Argument<NonNullGraphType<IntGraphType>>(name: "userProductId", "")
                .ResolveAsync(async ctx =>
                {
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    return (CreditLine) (await dbContext.CreditLines.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
                });

            Field<LoanType>()
                .Name("Loan")
                .Argument<NonNullGraphType<IntGraphType>>(name: "userProductId", "").ResolveAsync(async ctx =>
                {
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    return (Loan) (await dbContext.Loans.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
                });

            Field<CheckingAccountType>()
                .Name("CheckingAccount")
                .Argument<NonNullGraphType<IntGraphType>>(name: "userProductId", "").ResolveAsync(async ctx =>
                {
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    return (CheckingAccount) (await dbContext.CheckingAccounts.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
                });

            Field<ListGraphType<CashflowType>>()
                .Name("Cashflows")
                .Argument<NonNullGraphType<ListGraphType<NonNullGraphType<StringGraphType>>>>(name: "productIds", "")
                .ResolveAsync(async ctx =>
                {
                    var ids = ctx.GetArgument<string[]>("productIds");
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    var cash = await dbContext.Cashflows.Where(x => ids.Contains(x.ProductId)).ToArrayAsync();
                    return cash.Select(x => (Cashflow) x).ToArray();
                });
        }
    }
}
