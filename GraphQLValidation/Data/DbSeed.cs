using GraphQLValidation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLValidation.Data
{
    public class DbSeed : IDisposable
    {
        private readonly Context _context;
        public DbSeed(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Seed()
        {
            //user-product-mappings and users not owned by this api, we only have the ids, references to other systems' data,
            //but the tables themselves belong to some another api, microservice-style
            //user1  // two loans, one credit
            //user2  // two checking, one credit
            //user3  // one loan , one checking
            //user4  //one credit, 
            //user5  // one checking, two credit

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            BuildLoans();
            BuildCheckingAccounts();
            BuildCreditLines();

             _context.SaveChanges();

            foreach (var id in _context.Loans.Select(x => x.Id).ToArray()) _context.Cashflows.AddRange(BuildCashflows(id));

            foreach (var id in _context.CheckingAccounts.Select(x => x.Id).ToArray()) _context.Cashflows.AddRange(BuildCashflows(id));

            foreach (var id in _context.CreditLines.Select(x => x.Id).ToArray()) _context.Cashflows.AddRange(BuildCashflows(id));

            _context.SaveChanges();
        }

        
        private void BuildCheckingAccounts()
        {
            _context.CheckingAccounts.AddRange(new CheckingAccount
            {
                Id = "checking1",
                CurrentBalance = 43726.47M,
                FeePerMonth = 25,
                FinancialInstitution = "GraphBank",
                IsGoodStanding = true,
                UserProductId = 1
            }, new CheckingAccount
            {
                Id = "checking2",
                CurrentBalance = 4326.47M,
                FeePerMonth = 5,
                FinancialInstitution = "BusinessBank",
                IsGoodStanding = true,
                UserProductId = 2
            }, new CheckingAccount
            {
                Id = "checking3",
                CurrentBalance = -10.99M,
                FinancialInstitution = "GraphBank",
                IsGoodStanding = false,
                UserProductId = 3
            }, new CheckingAccount
            {
                Id = "checking4",
                CurrentBalance = 27.32M,
                FeePerMonth = 25,
                FinancialInstitution = "OtherBank",
                IsGoodStanding = true,
                UserProductId = 4
            });
        }

        private void BuildCreditLines()
        {
            _context.CreditLines.AddRange(new CreditLine
            {
                Id = "credit1",
                UserProductId = 5,
                Creditor = "PersonBank",
                CurrentBalance = 2347.2M,
                FeePerMonth = .02,
                IsGoodStanding = true,
                Line = 10000
            }, new CreditLine
            {
                Id = "credit2",
                UserProductId = 6,
                Creditor = "GraphBank",
                CurrentBalance = 12347.2M,
                FeePerMonth = .02,
                IsGoodStanding = true,
                Line = 20000
            }, new CreditLine
            {
                Id = "credit3",
                UserProductId = 7,
                Creditor = "BusinessBank",
                CurrentBalance = 2347.2M,
                FeePerMonth = .02,
                IsGoodStanding = true,
                Line = 15000
            }, new CreditLine
            {
                Id = "credit4",
                UserProductId = 8,
                Creditor = "BusinessBank",
                CurrentBalance = 7347.2M,
                InterestPerMonth = .279,
                IsGoodStanding = true,
                Line = 15000
            }, new CreditLine
            {
                Id = "credit5",
                UserProductId = 9,
                Creditor = "CreditBank",
                CurrentBalance = 7347.2M,
                InterestPerMonth = .279,
                IsGoodStanding = true,
                Line = 15000
            });
        }

        private void BuildLoans()
        {
            _context.Loans.AddRange(new Loan
            {
                Id = "loan1",
                UserProductId = 10,
                CurrentBalance = 23254.73M,
                Lender = "IronBank",
                FeePerMonth = 0.025D,
                IsGoodStanding = true,
                Principal = 30000,
                TermMonths = 18
            }, new Loan
            {
                Id = "loan2",
                UserProductId = 11,
                CurrentBalance = 1323.12M,
                Lender = "IronBank",
                FeePerMonth = 0.025D,
                IsGoodStanding = true,
                Principal = 10000,
                TermMonths = 6
            }, new Loan
            {
                Id = "loan3",
                UserProductId = 12,
                CurrentBalance = 13623.12M,
                Lender = "SwissBank",
                InterestRatePerYear = .04532D,
                IsGoodStanding = true,
                Principal = 50000,
                TermMonths = 6
            });
        }

        private IEnumerable<Cashflow> BuildCashflows(string productId)
        {
            return new[]
            {
                new Cashflow
                {
                    ProductId = productId,
                    Amount = 123.22M,
                    CurrencyCode = "USD",
                    Description = "Credit",
                    TransactionDate = null
                },
                new Cashflow
                {
                    ProductId = productId,
                    Amount = 123.22M,
                    CurrencyCode = "USD",
                    Description = "Debit",
                    TransactionDate = DateTime.UtcNow.AddDays(-3)
                },
                new Cashflow
                {
                    ProductId = productId,
                    Amount = 123.22M,
                    CurrencyCode = "USD",
                    Description = "Debit",
                    TransactionDate = DateTime.UtcNow.AddDays(-3)
                },
                new Cashflow
                {
                    ProductId = productId,
                    Amount = 5.00M,
                    CurrencyCode = "USD",
                    Description = "Fee",
                    TransactionDate = DateTime.UtcNow.AddDays(-4)
                },
                new Cashflow
                {
                    ProductId = productId,
                    Amount = 1442.42M,
                    CurrencyCode = "USD",
                    Description = "Credit",
                    TransactionDate = DateTime.UtcNow.AddDays(-5)
                }
            };
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}