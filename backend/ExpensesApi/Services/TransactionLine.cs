public record TransactionLine
{
    public string Dato { get; set; } = "";
    public string Forklaring { get; set; } = "";
    public decimal? Ut { get; set; }
    public decimal? Inn { get; set; }
}
