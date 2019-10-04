using System;
using System.Collections.Immutable;
using GraphQL.Types;
using GraphQLValidation.Data;
using GraphQLValidation.GraphQl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GraphQL;
using GraphQL.Authorization;

namespace GraphQLValidation.GraphQl.Types
{
    public class CheckingAccountType : ObjectGraphType<CheckingAccount>
    {
        public CheckingAccountType( IHttpContextAccessor accessor)
        {
            Name = "CheckingAccount";

            //this.AuthorizeWith("AdminPolicy");
            Field(x => x.UserProductId);
            Field(x => x.Id);
            Field<DecimalGraphType>().Name("currentBalance").Resolve(ctx =>
            {
                return ctx.Source.CurrentBalance;
            }).AuthorizeWith("AdminPolicy");
            Field(x => x.FeePerMonth);
            Field(x => x.FinancialInstitution);
            Field(x => x.IsGoodStanding);

            Field<ListGraphType<CashflowType>>()
                .Name("Cashflows")
                .ResolveAsync(async ctx =>
            {
                var dbContext = (Context)accessor.HttpContext.RequestServices.GetService(typeof(IContext));
                return (await dbContext.Cashflows.Where(x => x.ProductId == ctx.Source.Id).ToArrayAsync()).Select(x => (Cashflow)x);
            });//.AuthorizeWith("AdminPolicy");
        }
    }
}
