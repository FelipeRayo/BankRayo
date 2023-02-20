using BankRayo.Models;
using BankRayo.Repository;
using BankRayo.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankRayo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRespository;
        private readonly ILogger _logger;

        public TransactionController(ITransactionRepository transactionRespository, ILogger<ClientController> logger)
        {
            _transactionRespository = transactionRespository;
            _logger = logger;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            try
            {
                var Transactions = await _transactionRespository.GetTransactionsAsync();

                return Ok(Transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int transactionId)
        {
            try
            {
                if (transactionId == 0)
                {
                    return BadRequest(Messages.TransactionIdNotValid);
                }

                var Accounts = await _transactionRespository.GetTransactionAsync(transactionId);

                return Ok(Accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }


        [HttpGet("reportes")]
        public async Task<ActionResult<IEnumerable<FinancialReport>>> Get([FromQuery] string start, [FromQuery] string end, [FromQuery] int clientId)
        {
            try
            {
                if (!DateTime.TryParseExact(start, "MM-dd-yyyy hh::mm:tt", new CultureInfo("es-CO"), DateTimeStyles.None, out DateTime startDate) 
                    || !DateTime.TryParseExact(end, "MM-dd-yyyy hh::mm:tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate) 
                    || startDate >= endDate || clientId == 0)
                {
                    return BadRequest(Messages.TransactionIdNotValid);
                }

                var financialReport = await _transactionRespository.GetFinancialReport(startDate, endDate, clientId);

                return Ok(financialReport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }
        }

        // POST api/<TransactionController>
        [HttpPost]
        public async Task<ActionResult<Transaction>> Post([FromBody] Transaction transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest(Messages.TransactionInvalid);
                }

                var Account = await _transactionRespository.CreateTransactionAsync(transaction);

                return Ok(Account);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, Messages.FundsInsufficient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, Messages.UnavailableService);
            }


        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> Delete(int transactionId)
        {
            try
            {
                if (transactionId == 0)
                {
                    return BadRequest(Messages.TransactionIdNotValid);
                }

                var account = await _transactionRespository.GetTransactionAsync(transactionId);

                if (account == null)
                {
                    return NotFound(Messages.TransactionInvalid);
                }

                await _transactionRespository.GetTransactionAsync(transactionId);

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
