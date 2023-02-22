using BankRayo.Entities.BusinessEntities;
using BankRayo.Entities.Models;
using BankRayo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankRayo.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankRayoDbContext _bankRayoDbContext;

        public TransactionRepository(BankRayoDbContext bankRayoDbContext)
        {
            _bankRayoDbContext = bankRayoDbContext;
        }

        public async Task<IEnumerable<FinancialReport>> GetFinancialReport(DateTime start, DateTime end, int clientId)
        {
            return await (from client in _bankRayoDbContext.Person
                          join account in _bankRayoDbContext.Account
                          on client.ClientId equals account.IdClient
                          join transaction in _bankRayoDbContext.Transactions
                          on account.Number equals transaction.NumberAccount
                          where transaction.Date >= start
                          && transaction.Date <= end
                          && client.ClientId == clientId
                          select new FinancialReport
                          {
                              Date = transaction.Date,
                              Name = client.Name,
                              Number = account.Number,
                              Type = account.Type,
                              InitialBalance = account.InitialBalance,
                              State = account.State,
                              Transaction = FormatTransaction(transaction.Type, transaction.Value),
                              Balance = transaction.Balance
                          }).ToListAsync();
        }

        private static decimal FormatTransaction(string typeTransaction, decimal value)
        {
            switch (typeTransaction)
            {
                case "Retiro":
                    Decimal.TryParse($"-{value}", out decimal result);

                    return result;

                case "Deposito":
                    return value;

                default: return 0;
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _bankRayoDbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionAsync(int transactionId)
        {
            var Transaction = await _bankRayoDbContext.Transactions.Where(Transaction => Transaction.Id == transactionId).FirstOrDefaultAsync();

            if (Transaction == null)
            {
                throw new ArgumentException();
            }

            return Transaction;
        }

        private async Task<decimal> GetLatestBalance(int numberAccount)
        {
            var query = (from transaction in _bankRayoDbContext.Transactions
                         where transaction.NumberAccount == numberAccount
                         orderby transaction.Date descending
                         select transaction.Balance);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            switch (transaction.Type)
            {
                case "Retiro":
                    var LastBalance = await GetLatestBalance(transaction.NumberAccount);
                    if (LastBalance < transaction.Value)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;

                case "Deposito":
                    transaction.Date = DateTime.Now;
                    _bankRayoDbContext.Transactions.Add(transaction);
                    break;

                default:
                    throw new ArgumentException();
            }

            await _bankRayoDbContext.SaveChangesAsync();


            return await GetTransactionAsync(transaction.Id);
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            using (var db = _bankRayoDbContext)
            {
                var _transaction = await (from Transaction in db.Transactions
                                          where Transaction.Id == transaction.Id
                                          select Transaction).FirstOrDefaultAsync();

                _transaction.Value = transaction.Value;
                _transaction.Date = transaction.Date;
                _transaction.Type = transaction.Type;
                _transaction.Balance = transaction.Balance;
                _transaction.NumberAccount = transaction.NumberAccount;
                _transaction.State = false;

                await db.SaveChangesAsync();
            }

            return await GetTransactionAsync(transaction.Id);
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            using (var db = _bankRayoDbContext)
            {
                var _transaction = await (from Transaction in db.Transactions
                                     where Transaction.Id == transaction.Id
                                     select Transaction).FirstOrDefaultAsync();

                _transaction.State = false;

                await db.SaveChangesAsync();
            }
        }
    }
}
