public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public required Category Category { get; set; }
    public string AccountSource { get; set; } = "";
    public Guid ImportBatchId { get; set; }
    public string? RawLine { get; set; }
    public DateTime CreatedAt { get; set; }
}
