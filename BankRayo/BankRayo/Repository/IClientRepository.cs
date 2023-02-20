﻿using BankRayo.Models;

namespace BankRayo.Repository
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
