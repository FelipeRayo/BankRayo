using BankRayo.Models;
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
            //return await (from person in _bankRayoDbContext.Persons
            //              join client in _bankRayoDbContext.Clients
            //              on person.Id equals client.ClientId
            //              select new Client
            //              {
            //                  Id = person.Id,
            //                  Name = person.Name,
            //                  Address = person.Address,
            //                  Age = person.Age,
            //                  Gender = person.Gender,
            //                  NumberPhone = person.NumberPhone,
            //                  Password = client.Password,
            //                  ClientId = client.ClientId,
            //                  State = client.State,
            //              }).ToListAsync();
            return await _bankRayoDbContext.Person.OfType<Client>().ToListAsync();
        }

        public async Task<Client> GetClientAsync(int clientId)
        {
            var Client = await _bankRayoDbContext.Person.OfType<Client>().Where(client => client.ClientId == clientId).FirstOrDefaultAsync();


            if (Client == null)
            {
                throw new ArgumentException();
            }

            return Client;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            //var person = new Person
            //{
            //    Name = client.Name,
            //    Address = client.Address,
            //    Age = client.Age,
            //    Gender = client.Gender,
            //    NumberPhone = client.NumberPhone
            //};

            //_bankRayoDbContext.Persons.Add(person);
            //await _bankRayoDbContext.SaveChangesAsync();

            var _client = new Client
            {
                Name = client.Name,
                Address = client.Address,
                Age = client.Age,
                Gender = client.Gender,
                NumberPhone = client.NumberPhone,
                Password = client.Password,
                State = client.State,
                ClientId = client.ClientId,
            };

            _bankRayoDbContext.Client.Add(_client);
            await _bankRayoDbContext.SaveChangesAsync();

            return await GetClientAsync(client.ClientId);

        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            _bankRayoDbContext.Entry(client).State = EntityState.Modified;
            await _bankRayoDbContext.SaveChangesAsync();

            return await GetClientAsync(client.ClientId);
        }

        public async Task DeleteClientAsync(Client Client)
        {
            _bankRayoDbContext.Remove(Client);
            await _bankRayoDbContext.SaveChangesAsync();
        }
    }
}
