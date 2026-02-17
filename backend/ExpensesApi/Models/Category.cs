public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsDefault { get; set; } = true;
    public List<Transaction> Transactions { get; set; } = [];
    public List<CategoryRule> Rules { get; set; } = [];
}
