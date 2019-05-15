using Microsoft.EntityFrameworkCore;

namespace LoanRepaymentApi.Models
{
    public class LoanRepaymentContext : DbContext
    {
        public LoanRepaymentContext(DbContextOptions<LoanRepaymentContext> options)
            : base(options)
            {

            }

            public DbSet<Loan> Loans { get; set; }

    }
}