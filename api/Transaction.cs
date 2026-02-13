public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; } = "";
    public decimal? Out { get; set; }
    public decimal? In { get; set; }
}
