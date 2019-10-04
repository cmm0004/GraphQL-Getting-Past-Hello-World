using GraphQLValidation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLValidation.Data
{
    public class Context : DbContext, IContext
    {
        private static bool _created = false;
        public Context(DbContextOptions options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        //{
        //    optionbuilder.UseSqlite(@"Data Source=.\Data.db");
        //}

        public DbSet<Cashflow> Cashflows { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditLine> CreditLines { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
    }
}
