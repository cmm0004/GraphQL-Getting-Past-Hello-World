using System.Linq;
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
                .Argument<NonNullGraphType<IntGraphType>>("userProductId", "")
                .ResolveAsync(async ctx =>
                {
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    return (CreditLine) await dbContext.CreditLines.Where(x => x.UserProductId == ctx.GetArgument("userProductId", 0)).FirstOrDefaultAsync();
                });

            Field<LoanType>()
                .Name("Loan")
                .Argument<NonNullGraphType<IntGraphType>>("userProductId", "").ResolveAsync(async ctx =>
                {
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    return (Loan) await dbContext.Loans.Where(x => x.UserProductId == ctx.GetArgument("userProductId", 0)).FirstOrDefaultAsync();
                });

            Field<CheckingAccountType>()
                .Name("CheckingAccount")
                .Argument<NonNullGraphType<IntGraphType>>("userProductId", "").ResolveAsync(async ctx =>
                {
                    var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                    return (CheckingAccount) await dbContext.CheckingAccounts.Where(x => x.UserProductId == ctx.GetArgument("userProductId", 0)).FirstOrDefaultAsync();
                });
        }
    }
}