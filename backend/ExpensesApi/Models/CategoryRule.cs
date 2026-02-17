public class CategoryRule
{
    public int Id { get; set; }
    public required string Keyword { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; } = null!;
    public string Source { get; set; } = "seed";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
