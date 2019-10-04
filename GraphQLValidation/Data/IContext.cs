using GraphQLValidation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLValidation.Data
{
    internal interface IContext
    {
        DbSet<Cashflow> Cashflows { get; }
        DbSet<Loan> Loans { get; }
        DbSet<CreditLine> CreditLines { get; }
        DbSet<CheckingAccount> CheckingAccounts { get; }
    }
}