using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BankRayo.Models
{
    public class BankRayoDbContext : DbContext
    {
        public BankRayoDbContext(DbContextOptions<BankRayoDbContext> options)
            :base(options)
        {
        }

        DbSet<Person> Persons { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Transactions> Transactions { get; set; }
    }
}
