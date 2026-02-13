using Microsoft.EntityFrameworkCore;

public class ExpensesDb(DbContextOptions<ExpensesDb> opts) : DbContext(opts)
{
    public DbSet<Transaction> Transactions { get; set; }
}
