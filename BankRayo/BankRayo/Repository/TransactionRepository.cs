using BankRayo.Models;
using BankRayo.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return await (from client in _bankRayoDbContext.Client
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

        private decimal FormatTransaction(string typeTransaction, decimal value)
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

        public async Task<Transaction> GetTransactionAsync(int TransactionId)
        {
            var Transaction = await _bankRayoDbContext.Transactions.Where(Transaction => Transaction.Id == TransactionId).FirstOrDefaultAsync();

            if (Transaction == null)
            {
                throw new ArgumentException();
            }

            return Transaction;
        }

        private async Task<decimal> GetLatestBalance(int NumberAccount)
        {
            var query= (from transaction in _bankRayoDbContext.Transactions
                          where transaction.NumberAccount == NumberAccount
                          orderby transaction.Date descending
                          select transaction.Balance);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction Transaction)
        {
            switch (Transaction.Type)
            {
                case "Retiro":
                    var LastBalance = await GetLatestBalance(Transaction.NumberAccount);
                    if(LastBalance < Transaction.Value)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;

                case "Deposito":
                    Transaction.Date = DateTime.Now;
                    _bankRayoDbContext.Transactions.Add(Transaction);
                    break;

                default: 
                    throw new ArgumentException();
            }

            await _bankRayoDbContext.SaveChangesAsync();


            return await GetTransactionAsync(Transaction.Id);
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction Transaction)
        {
            _bankRayoDbContext.Entry(Transaction).State = EntityState.Modified;
            await _bankRayoDbContext.SaveChangesAsync();

            return await GetTransactionAsync(Transaction.Id);
        }

        public async Task DeleteTransactionAsync(Transaction Transaction)
        {
            _bankRayoDbContext.Remove(Transaction);
            await _bankRayoDbContext.SaveChangesAsync();
        }
    }
}
