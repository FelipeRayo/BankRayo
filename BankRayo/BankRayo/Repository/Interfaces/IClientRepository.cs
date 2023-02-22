using BankRayo.Entities.BusinessEntities;

namespace BankRayo.Repository.Interfaces
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetClientsAsync();
        public Task<Client> GetClientAsync(int clientId);
        public Task<Client> CreateClientAsync(Client client);
        public Task<Client> UpdateClientAsync(Client client);
        public Task DeleteClientAsync(Client clientId);
    }
}
