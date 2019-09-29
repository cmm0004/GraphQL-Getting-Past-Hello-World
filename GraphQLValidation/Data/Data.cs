using System;
using System.Linq;

namespace GraphQLValidation.Data
{
    public static class Data
    {

        public static UserProduct? GetUpById(string id)
        {
            return UserProducts.FirstOrDefault(x => x.UserProductId == id);
        }

        public static UserProduct[] GetUserProducts()
        {
            return UserProducts;
        }

        public static Loan[] GetLoansForUser(string userProductId, string? loanId = null)
        {
            if (loanId is null)
            {
                return Loans.Where(x => x.UserProductId == userProductId).ToArray();
            }

            return Loans.Where(x => x.UserProductId == userProductId && x.LoanId == loanId).ToArray();
        }

        public static CashAdvance[] GetCashAdvancesForUser(string userProductId, string? caId = null)
        {
            if (caId is null)
            {
                return CashAdvances.Where(x => x.UserProductId == userProductId).ToArray();
            }

            return CashAdvances.Where(x => x.UserProductId == userProductId && x.CashAdvanceId == caId).ToArray();
        }

        public static UserProduct[] UserProducts => new UserProduct[]
        {
            new UserProduct {IsGoodStanding = true, UserId = "candice", UserProductId = "candicesloan"},
            new UserProduct {IsGoodStanding = false, UserId = "candice", UserProductId = "candicesloan2"},
            new UserProduct {IsGoodStanding = true, UserId = "jonathan", UserProductId = "jonathanscashadvance"},
            new UserProduct {IsGoodStanding = true, UserId = "jonathan", UserProductId = "jonathansloan"}
        };

        public static Loan[] Loans => new Loan[]
        {
            new Loan
            {
                Amount = 40000, UserProductId = "candicesloan", CurrencyCode = "USD", InterestRate = 2.23, Lender = "monopolyman", LoanId = "loan1",
                OriginationDate = new DateTime(2019, 01, 01)
            },
            new Loan
            {
                Amount = 10000, UserProductId = "candicesloan2", CurrencyCode = "USD", Fee = 4.0, Lender = "monopolyman", LoanId = "loan2",
                OriginationDate = new DateTime(2019, 07, 01)
            },
            new Loan
            {
                Amount = 20000, UserProductId = "jonathansloan", CurrencyCode = "USD", Fee = 4.0, Lender = "monopolyman", LoanId = "loan3",
                OriginationDate = new DateTime(2019, 05, 01)
            }
        };

        public static CashAdvance[] CashAdvances => new CashAdvance[]
        {
            new CashAdvance
            {
                Amount = 10000, Fee = 3.43, CashAdvanceId = "cashadvance1", CurrencyCode = "USD", UserProductId = "jonathanscashadvance",
                OriginationDate = new DateTime(2018, 04, 03), DateDue = new DateTime(2019, 01, 01)
            }

        };
    }
}

    public enum ProductType
    {
        Loan = 0,
        CashAdvance = 1

    }
    public class Loan
    {
        public string Type => ProductType.Loan.ToString();
        public string LoanId { get; set; }
        public string UserProductId { get; set; }
        public DateTime OriginationDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Lender { get; set; }
        public double? InterestRate { get; set; }
        public double? Fee { get; set; }
    }

    public class CashAdvance
    {
        public string Type => ProductType.CashAdvance.ToString();
        public string CashAdvanceId { get; set; }
        public string UserProductId { get; set; }
        public DateTime OriginationDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime DateDue { get; set; }
        public double? InterestRate { get; set; }
        public double? Fee { get; set; }
    }

    public class UserProduct
    {
        public string UserProductId { get; set; }
        public string UserId { get; set; }
        public bool IsGoodStanding { get; set; }
    }
