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

    static string GetMerchant(string raw)
    {
        var visa = Regex.Match(raw, @"^Visa \d+ (?:Nok \d+,\d+ )?(.+?)(?:\s+:\S+)?(?:\s+\d+)?$");
        if (visa.Success)
            return visa.Groups[1].Value;

        var butikk = Regex.Match(
            raw,
            @"^Varekjøp i butikk (.+) [A-ZÆØÅ]+ F Reservert transaksjon$"
        );
        if (butikk.Success)
            return butikk.Groups[1].Value;

        var avd = Regex.Match(raw, @"^Varekjøp (.+?) Avd \d+");
        if (avd.Success)
            return avd.Groups[1].Value;

        var overforing = Regex.Match(raw, @"^\S+ \S+ \d+ (\p{Lu}\p{Ll}+ \p{Lu}\p{Ll}+)");
        if (overforing.Success)
            return $"Overføring - {overforing.Groups[1].Value}";

        var konto = Regex.Match(raw, @"^Kontoregulering \d+ (\S+)$");
        if (konto.Success)
            return $"Kontoregulering - {konto.Groups[1].Value}";

        return raw;
    }

    public ParsedRow? ParseLine(string l)
    {
        var lArr = l.Replace("\"", "").Split(";");

        if (lArr.Length < 5 || ParseDate(lArr[0]) is not DateOnly date)
            return null;

        var description = ParseDescription(lArr[1]);
        var merchant = GetMerchant(description);

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
