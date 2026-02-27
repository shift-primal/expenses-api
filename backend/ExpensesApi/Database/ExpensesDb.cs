using Microsoft.EntityFrameworkCore;

public class ExpensesDb(DbContextOptions<ExpensesDb> opts) : DbContext(opts)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryRule> CategoryRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData.Seed(modelBuilder);

        modelBuilder.Entity<Transaction>().HasIndex(t => t.Date);

        modelBuilder.Entity<Transaction>().HasIndex(t => t.CategoryId);

        modelBuilder.Entity<Transaction>().HasIndex(t => t.Merchant);

        modelBuilder.Entity<Transaction>().HasIndex(t => t.Amount);
    }
}
