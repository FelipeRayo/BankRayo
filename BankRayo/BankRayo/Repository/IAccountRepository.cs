﻿using BankRayo.Models;

namespace BankRayo.Repository
{
    public interface IAccountRepository
    {
        public Task<IEnumerable<Account>> GetAccountsAsync();
        public Task<Account> GetAccountAsync(int AccountId);
        public Task<Account> CreateAccountAsync(Account Account);
        public Task<Account> UpdateAccountAsync(Account Account);
        public Task DeleteAccountAsync(Account Account);
    }
}
