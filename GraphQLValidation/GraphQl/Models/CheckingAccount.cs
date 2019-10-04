using System;

namespace GraphQLValidation.GraphQl.Models
{
    public class CheckingAccount
    {
        public string Id { get; set; }
        public int UserProductId { get; set; }
        public string FinancialInstitution { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }

        public static implicit operator CheckingAccount(Data.Entities.CheckingAccount checking)
        {
            return new CheckingAccount
            {
                Id = checking.Id.ToString(),
                UserProductId = checking.UserProductId,
                CurrentBalance = checking.CurrentBalance,
                FeePerMonth = checking.FeePerMonth,
                FinancialInstitution = checking.FinancialInstitution,
                IsGoodStanding = checking.IsGoodStanding
            };
        }
    }
}
