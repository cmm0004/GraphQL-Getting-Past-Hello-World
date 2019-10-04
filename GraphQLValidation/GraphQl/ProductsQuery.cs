using GraphQL.Types;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl.Models;
using GraphQLValidation.GraphQl.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GraphQLValidation.GraphQl
{
    [ExcludeFromCodeCoverage]
    public class ProductsQuery : ObjectGraphType
    {
        public ProductsQuery(IHttpContextAccessor accessor)
        {
            Name = "Products";
            Field<CreditLineType>()
                .Name("CreditLine")
                .Argument<NonNullGraphType<IntGraphType>>(name: "userProductId", "")
                .ResolveAsync(async ctx =>
            {
                var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                return (CreditLine)(await dbContext.CreditLines.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
            });
            
            Field<LoanType>()
                .Name("Loan")
                .Argument<NonNullGraphType<IntGraphType>>(name: "userProductId", "").ResolveAsync(async ctx =>
            {
                var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                return (Loan)(await dbContext.Loans.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
            });
            
            Field<CheckingAccountType>()
                .Name("CheckingAccount")
                .Argument<NonNullGraphType<IntGraphType>>(name: "userProductId", "").ResolveAsync(async ctx =>
            {
                var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                return (CheckingAccount) (await dbContext.CheckingAccounts.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
            });

            Field<ListGraphType<CashflowType>>()
                .Name("Cashflows")
                .Argument<ListGraphType<NonNullGraphType<GuidGraphType>>>(name: "productId", "")
                .ResolveAsync(async ctx =>
            {
                var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));

                return (await dbContext.Cashflows.Where(x => ((GraphQLUserContext)ctx.UserContext).RequestedProductIds.Contains(x.ProductId))
                    .ToArrayAsync())
                    .Select(x => (Cashflow)x);
            });
        }
    }
}