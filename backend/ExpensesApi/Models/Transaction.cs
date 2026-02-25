public class Transaction
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public required string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid ImportBatchId { get; set; }
    public DateTime CreatedAt { get; set; }
}
