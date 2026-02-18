public class Transaction
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public required string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public string AccountSource { get; set; } = "";
    public Guid ImportBatchId { get; set; }
    public string? RawLine { get; set; }
    public DateTime CreatedAt { get; set; }
}
