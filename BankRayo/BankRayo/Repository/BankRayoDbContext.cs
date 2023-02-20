using BankRayo.Models;
using Microsoft.EntityFrameworkCore;

namespace BankRayo.Repository
{
    public class BankRayoDbContext : DbContext
    {
        public BankRayoDbContext(DbContextOptions<BankRayoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
