using GraphQLValidation.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GraphQLValidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var seed = new DbSeed(new Context(options: new DbContextOptionsBuilder().UseSqlite(@"Data Source=.\Data\Data.db").Options)))
            {
                seed.Seed();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
