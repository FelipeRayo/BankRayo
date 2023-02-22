using BankRayo.Entities.BusinessEntities;
using BankRayo.Entities.Models;

namespace BankRayo.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<IEnumerable<FinancialReport>> GetFinancialReport(DateTime start, DateTime end, int clientId);
        public Task<IEnumerable<Transaction>> GetTransactionsAsync();
        public Task<Transaction> GetTransactionAsync(int TransactionId);
        public Task<Transaction> CreateTransactionAsync(Transaction Transaction);
        public Task<Transaction> UpdateTransactionAsync(Transaction Transaction);
        public Task DeleteTransactionAsync(Transaction TransactionId);
    }
}
