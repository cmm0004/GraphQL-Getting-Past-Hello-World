using System;
using System.Linq;
using GraphQL.Types;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GraphQLValidation.GraphQl.Types
{
    public class CreditLineType : ObjectGraphType<CreditLine>
    {
        public CreditLineType(IHttpContextAccessor accessor)
        {
            Name = "CreditLine";
            Field(c => c.UserProductId);
            Field(c => c.Id);
            Field(c => c.Creditor);
            Field(c => c.CurrentBalance);
            Field(c => c.FeePerMonth);
            Field(c => c.InterestPerMonth);
            Field(c => c.IsGoodStanding);
            Field(c => c.Line);

            Field<ListGraphType<CashflowType>>()
                .Name("Cashflows")
                .ResolveAsync(async ctx =>
            {
                var dbContext = (Context) accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                return (await dbContext.Cashflows.Where(x => x.ProductId == Guid.Parse(ctx.Source.Id)).ToArrayAsync()).Select(x => (Cashflow)x);
            });
        }
    }
}
