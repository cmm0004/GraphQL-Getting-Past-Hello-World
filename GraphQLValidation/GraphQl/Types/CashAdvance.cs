using GraphQL.Types;

namespace GraphQLValidation.GraphQl.Types
{
    public class CashAdvanceQuery : ObjectGraphType<CashAdvance>
    {
        public CashAdvanceQuery()
        {
            Name = "CashAdvance";

            Field(u => u.CashAdvanceId, type: typeof(IdGraphType));
            Field(u => u.UserProductId);
            Field(u => u.Fee, true);
            Field(u => u.Amount);
            Field(u => u.CurrencyCode);
            Field(u => u.InterestRate, true);
            Field(u => u.DateDue);
            Field(u => u.OriginationDate);
            Field(u => u.Type);
        }
    }
}
