using BankRayo.Models;
using BankRayo.Repository;
using BankRayo.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankRayo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRespository;
        private readonly ILogger _logger;

        public AccountController(IAccountRepository accounttRespository, ILogger<ClientController> logger)
        {
            _accountRespository = accounttRespository;
            _logger = logger;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            try
            {
                var Accounts = await _accountRespository.GetAccountsAsync();

                return Ok(Accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int accountId)
        {
            try
            {
                if (accountId == 0)
                {
                    return BadRequest(Messages.AccountNumberNotValid);
                }

                var Accounts = await _accountRespository.GetAccountAsync(accountId);

                return Ok(Accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] Account account)
        {
            try
            {
                if (account == null)
                {
                    return BadRequest(Messages.AccountInvalid);
                }

                var Account = await _accountRespository.CreateAccountAsync(account);

                return Ok(Account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut("{accountId}")]
        public async Task<IActionResult> Put(int accountId, [FromBody] Account account)
        {
            try
            {
                if (accountId != account.Number)
                {
                    return BadRequest(Messages.AccountNumberNotValid);
                }

                if (account == null)
                {
                    return BadRequest(Messages.AccountInvalid);
                }

                if(_accountRespository.GetAccountAsync(accountId) == null){
                    return NotFound(Messages.AccountNotFound);
                };

                await _accountRespository.UpdateAccountAsync(account);

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
                    return BadRequest(Messages.AccountNumberNotValid);
                }

                var account = await _accountRespository.GetAccountAsync(accountId);

                if (account == null)
                {
                    return NotFound(Messages.AccountNotFound);
                }

                await _accountRespository.DeleteAccountAsync(account);

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
