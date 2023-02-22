using BankRayo.Entities.BusinessEntities;
using BankRayo.Repository.Interfaces;
using BankRayo.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankRayo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRespository;
        private readonly ILogger _logger;

        public ClientController(IClientRepository clientRespository, ILogger<ClientController> logger)
        {
            _clientRespository = clientRespository;
            _logger = logger;
        }

        //GET api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                var Clients = await _clientRespository.GetClientsAsync();

                return Ok(Clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // GET api/<ClientController>/5
        [HttpGet("{clientId}")]
        public async Task<ActionResult<Client>> Get(int clientId)
        {
            try
            {
                if (clientId == 0)
                {
                    return BadRequest(Messages.ClientIdNotValid);
                }

                var Accounts = await _clientRespository.GetClientAsync(clientId);

                return Ok(Accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }


        //POST api/Client
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody] Client client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest();
                }

                var Client = await _clientRespository.CreateClientAsync(client);

                return Ok(Client);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        // PUT api/<AccountController>/5
        [HttpPut("{accountId}")]
        public async Task<IActionResult> Put(int accountId, [FromBody] Client client)
        {
            try
            {
                if (accountId != client.ClientId)
                {
                    return BadRequest(Messages.ClientIdNotValid);
                }

                if (client == null)
                {
                    return BadRequest(Messages.ClientInvalid);
                }

                if (_clientRespository.GetClientAsync(accountId) == null)
                {
                    return NotFound(Messages.ClientNotFound);
                };

                await _clientRespository.UpdateClientAsync(client);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Delete(int accountId)
        {
            try
            {
                if (accountId == 0)
                {
                    return BadRequest(Messages.ClientIdNotValid);
                }

                var account = await _clientRespository.GetClientAsync(accountId);

                if (account == null)
                {
                    return NotFound(Messages.ClientNotFound);
                }

                await _clientRespository.DeleteClientAsync(account);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }
    }
}
