using System;

namespace GraphQLValidation.GraphQl.Models
{
    public class CreditLine
    {
        public string Id { get; set; }
        public int UserProductId { get; set; }
        public string Creditor { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public decimal Line { get; set; }
        public double InterestPerMonth { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }

        public static implicit operator CreditLine(Data.Entities.CreditLine credit)
        {
            return new CreditLine
            {
                Id = credit.Id.ToString(),
                UserProductId = credit.UserProductId,
                Creditor = credit.Creditor,
                CurrentBalance = credit.CurrentBalance,
                FeePerMonth = credit.FeePerMonth,
                InterestPerMonth = credit.InterestPerMonth,
                IsGoodStanding = credit.IsGoodStanding,
                Line = credit.Line
            };
        }

    }
}
