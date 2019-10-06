using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl.Models;
using GraphQLValidation.GraphQl.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
                    return (CheckingAccount)(await dbContext.CheckingAccounts.Where(x => x.UserProductId == ctx.GetArgument<int>("userProductId", 0)).FirstOrDefaultAsync());
                });

            Field<ListGraphType<CashflowType>>()
                .Name("Cashflows")
                .Argument<NonNullGraphType<ListGraphType<NonNullGraphType<StringGraphType>>>>(name: "productIds", "")
                .ResolveAsync(async ctx =>
                {
                    // it seems more correct to block entire object if any of the ids were unauthorized, though not the entire call.
                    // nothing stopping you from just getting items for the ids that are valid, though would likely lead to some client confusion
                    var args = ctx.GetArgument("productIds", new List<string>());
                    var validated = ((GraphQLUserContext)ctx.UserContext).RequestedProductIds;
                    var diff = args.Where(p => !validated.Contains(p));
                    if (diff.Any())
                    {
                        ctx.Errors.Add(new ExecutionError($"not all requested ids {string.Join(", ", args)} were valid for user {((GraphQLUserContext)ctx.UserContext).UserId}"));
                        return new Cashflow[0];
                    }
                    var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    var cash = await dbContext.Cashflows.Where(x => args.Contains(x.ProductId)).ToArrayAsync();
                    return cash.Select(x => (Cashflow)x).ToArray();
                });
        }
    }
}
