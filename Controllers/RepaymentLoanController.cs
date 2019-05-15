using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanRepaymentApi.Models;

namespace LoanRepaymentApi.Controllers

{
    [Route("api/loans")]
    [ApiController]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly LoanRepaymentContext _context;

        public LoanRepaymentController(LoanRepaymentContext context)
        {
            _context = context;

            if (_context.Loans.Count() == 0)
            {

                _context.Loans.Add(new Loan { Name = "Loan 1", Repayment = "£32,000" });
                _context.SaveChanges();
            }
        }

        //GET Request

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            return await _context.Loans.ToListAsync();
        }

        // GET id 
        [HttpGet("{id}")]

        public async Task<ActionResult<Loan>> GetLoan(long id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        //POST request

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoan), new Loan { id = loan.id }, loan);
        }

        //PUT Request

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(long id, Loan loan)
        {
            if (id != loan.id)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(long id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
