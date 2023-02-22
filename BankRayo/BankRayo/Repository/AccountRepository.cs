using BankRayo.Entities.Models;
using BankRayo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankRayo.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankRayoDbContext _bankRayoDbContext;

        public AccountRepository(BankRayoDbContext bankRayoDbContext)
        {
            _bankRayoDbContext = bankRayoDbContext;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _bankRayoDbContext.Account.ToListAsync();
        }

        public async Task<Account> GetAccountAsync(int number)
        {
            var Account = await _bankRayoDbContext.Account.Where(Account => Account.Number == number).FirstOrDefaultAsync();

            if (Account == null)
            {
                throw new ArgumentException();
            }

            return Account;
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _bankRayoDbContext.Account.Add(account);
            await _bankRayoDbContext.SaveChangesAsync();

            if (account.InitialBalance > 0)
            {
                var transaction = new Transaction
                {
                    NumberAccount = account.Number,
                    Date = DateTime.Now,
                    Type = "Deposito",
                    Value = account.InitialBalance,
                    Balance = account.InitialBalance,
                    State = true
                };

                _bankRayoDbContext.Transactions.Add(transaction);
                await _bankRayoDbContext.SaveChangesAsync();
            }

            return await GetAccountAsync(account.Number);
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            using (var db = _bankRayoDbContext)
            {
                var _account = await (from Account in db.Account
                                      where Account.Number == account.Number
                                      select Account).FirstOrDefaultAsync();

                _account.State = false;

                await db.SaveChangesAsync();
            }

            return await GetAccountAsync(account.Number);
        }

        public async Task DeleteAccountAsync(Account account)
        {
            using (var db = _bankRayoDbContext)
            {
                var _account = await (from Account in db.Account
                                      where Account.Number == account.Number
                                      select Account).FirstOrDefaultAsync();

                _account.State = false; 
                
                await db.SaveChangesAsync();
            }
        }
    }
}
