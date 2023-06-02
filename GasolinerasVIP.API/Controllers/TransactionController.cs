using GasolinerasVIP.API.Models;
using GuiaDCEA.API.Data;
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
            context.Add(transaction);
            await context.SaveChangesAsync();
            return transaction.Id;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            var transaction =  await context.Transaction.FindAsync(id);
            if(transaction == null) { return NotFound(); }
            return transaction;
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
