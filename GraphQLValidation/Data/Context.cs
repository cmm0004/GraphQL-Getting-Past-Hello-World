using GraphQLValidation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLValidation.Data
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cashflow> Cashflows { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditLine> CreditLines { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
    }
}
