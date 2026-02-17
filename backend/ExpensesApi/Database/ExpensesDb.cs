using Microsoft.EntityFrameworkCore;

public class ExpensesDb(DbContextOptions<ExpensesDb> opts) : DbContext(opts)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryRule> CategoryRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData.Seed(modelBuilder);
    }
}
