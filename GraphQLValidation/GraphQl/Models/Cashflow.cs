using System;

namespace GraphQLValidation.GraphQl.Models
{
    public class Cashflow
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } = "";
        public DateTime? TransactionDate { get; set; }

        public static implicit operator Cashflow(Data.Entities.Cashflow cashflow)
        {
            return new Cashflow
            {
                Amount = cashflow.Amount,
                CurrencyCode = cashflow.CurrencyCode,
                Description = cashflow.Description,
                Id = cashflow.Id.ToString(),
                ProductId = cashflow.ProductId.ToString(),
                TransactionDate = cashflow.TransactionDate
            };
        }
    }
}
