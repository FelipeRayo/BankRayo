using BankRayo.Models;
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

        public async Task<Account> GetAccountAsync(int Number)
        {
            var Account = await _bankRayoDbContext.Account.Where(Account => Account.Number == Number).FirstOrDefaultAsync();

            if (Account == null)
            {
                throw new ArgumentException();
            }

            return Account;
        }

        public async Task<Account> CreateAccountAsync(Account Account)
        {
            _bankRayoDbContext.Account.Add(Account);
            await _bankRayoDbContext.SaveChangesAsync();

            if (Account.InitialBalance > 0)
            {
                var transaction = new Transaction
                {
                    NumberAccount = Account.Number,
                    Date = DateTime.Now,
                    Type = "Deposito",
                    Value = Account.InitialBalance,
                    Balance = Account.InitialBalance,
                    State = true
                };

                _bankRayoDbContext.Transactions.Add(transaction);
                await _bankRayoDbContext.SaveChangesAsync();
            }

            return await GetAccountAsync(Account.Number);
        }

        public async Task<Account> UpdateAccountAsync(Account Account)
        {
            _bankRayoDbContext.Entry(Account).State = EntityState.Modified;
            await _bankRayoDbContext.SaveChangesAsync();

            return await GetAccountAsync(Account.Number);
        }

        public async Task DeleteAccountAsync(Account Account)
        {

            using (var db = _bankRayoDbContext)
            {
                var _account = await (from account in db.Account
                                      where account.Number == Account.Number
                                      select account).FirstOrDefaultAsync();

                _account.State = false; 
                
                await db.SaveChangesAsync();
            }
        }
    }
}
