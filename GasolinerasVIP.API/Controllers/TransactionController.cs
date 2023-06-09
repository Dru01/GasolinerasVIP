using GasolinerasVIP.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    

namespace GasolinerasVIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TransactionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostTransaction(Transaction transaction)
        {
            transaction.GasStation = await context.GasStation.SingleAsync(t => t.Id == transaction.GasStationId);
            context.Transaction.Add(transaction);
            await context.SaveChangesAsync();
            transaction = await context.Transaction.Include(e => e.GasStation).FirstAsync(t => t.Id == transaction.Id);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetTransactionList()
        {
            return await context.Transaction.Include(e => e.GasStation).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            if (!TransactionExists(id)) { return NotFound(); }
            return await context.Transaction.Include(e => e.GasStation).SingleAsync(i => i.Id == id);
        }

        private bool TransactionExists(int id)
        {
            return context.Transaction.Any(e => e.Id == id);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
                return BadRequest();

            context.Entry(transaction).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
