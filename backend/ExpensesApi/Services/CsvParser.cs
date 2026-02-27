using System.Globalization;
using System.Text.RegularExpressions;

public class CsvParser
{
    static decimal ParseAmount(string raw, bool negate) =>
        decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var r)
            ? (negate ? -r : r)
            : 6767;

    static DateOnly? ParseDate(string raw) => DateOnly.TryParse(raw, out var r) ? r : null;

    static string ParseDescription(string raw) => Regex.Replace(raw, @"\s{2,}", " ").Trim();

    public ParsedRow? ParseLine(string l)
    {
        var lArr = l.Replace("\"", "").Split(";");

        if (lArr.Length < 5 || ParseDate(lArr[0]) is not DateOnly date)
            return null;

        var merchant = MerchantParser.Parse(lArr[1]);
        var description = ParseDescription(lArr[1]);

        bool isExpense = string.IsNullOrEmpty(lArr[4]);
        var rawAmt = isExpense ? lArr[3] : lArr[4];
        decimal amount = ParseAmount(rawAmt, isExpense);

        return new ParsedRow(date, description, merchant, amount);
    }

    public List<ParsedRow> ParseRows(StreamReader reader)
    {
        reader.ReadLine(); // Skip header

        var rows = new List<ParsedRow>();
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            var row = ParseLine(line);
            if (row is not null)
                rows.Add(row);
        }

        return rows;
    }
}
