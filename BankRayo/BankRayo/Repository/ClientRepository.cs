using BankRayo.Entities.BusinessEntities;
using BankRayo.Entities.Models;
using BankRayo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankRayo.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankRayoDbContext _bankRayoDbContext;

        public ClientRepository(BankRayoDbContext bankRayoDbContext)
        {
            _bankRayoDbContext = bankRayoDbContext;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await (from person in _bankRayoDbContext.Person
                          select new Client
                          {
                              Id = person.Id,
                              Name = person.Name,
                              Address = person.Address,
                              Age = person.Age,
                              Gender = person.Gender,
                              NumberPhone = person.NumberPhone,
                              Password = person.Password,
                              ClientId = person.ClientId,
                              State = person.State,
                          }).ToListAsync();
        }

        public async Task<Client> GetClientAsync(int clientId)
        {
            var Client = await (from person in _bankRayoDbContext.Person
                                where person.ClientId == clientId
                                select new Client
                                {
                                    Id = person.Id,
                                    Name = person.Name,
                                    Address = person.Address,
                                    Age = person.Age,
                                    Gender = person.Gender,
                                    NumberPhone = person.NumberPhone,
                                    Password = person.Password,
                                    ClientId = person.ClientId,
                                    State = person.State,
                                }).FirstOrDefaultAsync();


            if (Client == null)
            {
                throw new ArgumentException();
            }

            return Client;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            var person = new Entities.Models.Person
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Gender = client.Gender,
                Age = client.Age,
                Address = client.Address,
                NumberPhone = client.NumberPhone,
                Password = client.Password,
                State = client.State
            };

            _bankRayoDbContext.Person.Add(person);
            await _bankRayoDbContext.SaveChangesAsync();

            return await GetClientAsync(client.ClientId);
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            using (var db = _bankRayoDbContext)
            {
                var _client = await (from Client in db.Person
                                     where Client.ClientId == client.ClientId
                                     select Client).FirstOrDefaultAsync();

                _client.Name = client.Name;
                _client.Gender = client.Gender;
                _client.Age = client.Age;
                _client.Address = client.Address;
                _client.NumberPhone = client.NumberPhone;
                _client.Password = client.Password;
                _client.State = false;

                await db.SaveChangesAsync();
            }

            return await GetClientAsync(client.ClientId);
        }

        public async Task DeleteClientAsync(Client client)
        {
            using (var db = _bankRayoDbContext)
            {
                var _client = await (from Client in db.Person
                                      where Client.ClientId == client.ClientId
                                      select Client).FirstOrDefaultAsync();

                _client.State = false;

                await db.SaveChangesAsync();
            }
        }
    }
}
