using System;

namespace GraphQLValidation.GraphQl.Models
{
    public class Loan 
    {
        public string Id { get; set; }
        public int UserProductId { get; set; }
        public string Lender { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public decimal Principal { get; set; }
        public int TermMonths { get; set; }
        public double InterestRatePerYear { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }

        public static implicit operator Loan(Data.Entities.Loan loan)
        {
            return new Loan
            {
                Id = loan.Id.ToString(),
                UserProductId = loan.UserProductId,
                CurrentBalance = loan.CurrentBalance,
                FeePerMonth = loan.FeePerMonth,
                InterestRatePerYear = loan.InterestRatePerYear,
                IsGoodStanding = loan.IsGoodStanding,
                Lender = loan.Lender,
                Principal = loan.Principal,
                TermMonths = loan.TermMonths
            };
        }
    }
}
