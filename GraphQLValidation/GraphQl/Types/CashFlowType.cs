using GraphQL.Types;
using GraphQLValidation.GraphQl.Models;

namespace GraphQLValidation.GraphQl.Types
{
    public class CashflowType : ObjectGraphType<Cashflow>
    {
        public CashflowType()
        {
            Name = "Cashflow";
            Field(c => c.Id);
            Field(c => c.ProductId);
            Field(c => c.Amount);
            Field(c => c.CurrencyCode);
            Field(c => c.Description);
            Field(c => c.TransactionDate, true);
        }
    }
}
