using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;

public class CsvParser
{
    private sealed class DnbLineMap : ClassMap<TransactionLine>
    {
        public DnbLineMap()
        {
            Map(x => x.Dato).Name("Dato");
            Map(x => x.Forklaring).Name("Forklaring");
            Map(x => x.Ut).Name("Ut fra konto");
            Map(x => x.Inn).Name("Inn på konto");
        }
    }

    private sealed class ValleLineMap : ClassMap<TransactionLine>
    {
        public ValleLineMap()
        {
            Map(x => x.Dato).Name("Betalingstidspunkt");
            Map(x => x.Forklaring).Name("Skildring");
            Map(x => x.Ut)
                .Name("Beløp ut")
                .Convert(r =>
                {
                    var raw = r.Row.GetField("Beløp ut");
                    return decimal.TryParse(
                        raw,
                        NumberStyles.Any,
                        CultureInfo.CurrentCulture,
                        out var v
                    )
                        ? -v
                        : (decimal?)null;
                });
            Map(x => x.Inn).Name("Beløp inn");
        }
    }

    static DateOnly? ParseDate(string raw) =>
        DateOnly.TryParseExact(raw, "dd.MM.yyyy", null, DateTimeStyles.None, out var r) ? r : null;

    static string ParseDescription(string raw) => Regex.Replace(raw, @"\s{2,}", " ").Trim();

    static ParsedRow? ToRow(TransactionLine line, Func<string, string> parseMerchant)
    {
        if (ParseDate(line.Dato) is not DateOnly date)
            return null;

        var description = ParseDescription(line.Forklaring);

        return new ParsedRow(
            Date: date,
            Description: description,
            Merchant: parseMerchant(description),
            Amount: line.Ut.HasValue ? -line.Ut.Value : line.Inn ?? 0
        );
    }

    public List<ParsedRow> Parse(StreamReader stream)
    {
        var content = stream.ReadToEnd();
        var firstLine = content.Split('\n')[0];

        var isValle = firstLine.Contains("Betalingstidspunkt");

        var cfg = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" };
        using var csv = new CsvReader(new StringReader(content), cfg);

        if (isValle)
            csv.Context.RegisterClassMap<ValleLineMap>();
        else
            csv.Context.RegisterClassMap<DnbLineMap>();

        Func<string, string> parseMerchant = isValle ? MerchantParser.ParseValle : MerchantParser.ParseDnb;

        return csv.GetRecords<TransactionLine>().Select(l => ToRow(l, parseMerchant)).Where(r => r is not null).ToList()!;
    }
}
