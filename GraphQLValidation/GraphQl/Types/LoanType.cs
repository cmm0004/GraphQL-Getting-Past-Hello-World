using System;
using System.Linq;
using GraphQL.Types;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GraphQLValidation.GraphQl.Types
{
    public class LoanType : ObjectGraphType<Loan>
    {
        public LoanType(IHttpContextAccessor accessor)
        {
            Name = "Loan";
            Field(l => l.UserProductId);
            Field(l => l.Id);
            Field(l => l.CurrentBalance);
            Field(l => l.FeePerMonth);
            Field(l => l.InterestRatePerYear);
            Field(l => l.IsGoodStanding);
            Field(l => l.Lender);
            Field(l => l.Principal);
            Field(l => l.TermMonths);

            Field<ListGraphType<CashflowType>>()
                .Name("Cashflows")
                .ResolveAsync(async ctx =>
            {
                var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                return (await dbContext.Cashflows.Where(x => x.ProductId == Guid.Parse(ctx.Source.Id)).ToArrayAsync()).Select(x => (Cashflow)x);
            });
        }
    }
}
