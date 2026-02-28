public class CsvParserValleTests
{
    private const string DnbHeader =
        "\"Dato\";\"Forklaring\";\"Rentedato\";\"Ut fra konto\";\"Inn på konto\"";

    private const string ValleHeader =
        "Betalingstidspunkt;Bokført dato;Valuteringsdato;Skildring;Type;Undertype;Frå konto;Avsendar;Til konto;Mottakarnamn;Beløp inn;Beløp ut;Valuta;Status;Melding/KID/Fakt.nr;eFaktura;eFaktura eier;eFaktura type;Melding;KID;Faktura nr.";

    public static TheoryData<string, DateOnly, string, decimal> DnbCases =>
        new()
        {
            {
                "\"23.01.2026\";\"Varekjøp Kiwi 002 Reinsv Bøverbruvege Reinsv Dato 23.01 kl. 14.39 \";\"23.01.2026\";407.8;\"\"",
                new DateOnly(2026, 1, 23),
                "Kiwi",
                -407.8m
            },
        };

    public static TheoryData<string, DateOnly, string, decimal> ValleCases =>
        new()
        {
            {
                "12.01.2026;12.01.2026;12.01.2026;09.01 COOP MARKED KOL BRUSTADVEGEN KOLBU;Varekjøp;Varekjøp debetkort;1234 56 78901;Ola Nordmann;;;;-86;NOK;Bokført;09.01 COOP MARKED KOL BRUSTADVEGEN KOLBU;;;;;;",
                new DateOnly(2026, 1, 12),
                "COOP MARKED",
                -86m
            },
            {
                "24.10.2025;24.10.2025;24.10.2025;GJENSIDIGE FORSIKRING ASA (98765432101);Betaling innland - Avtalegiro;Betaling med KID innland;1234 56 78902;Brukskonto;1234 56 78903;GJENSIDIGE FORSIKRING ASA;;-1413;NOK;Bokført;GJENSIDIGE FORSIKRING ASA;;;;;;",
                new DateOnly(2025, 10, 24),
                "GJENSIDIGE FORSIKRING ASA",
                -1413m
            },
        };

    [Theory]
    [MemberData(nameof(DnbCases))]
    public void Dnb_ParsesCorrectly(
        string rawLine,
        DateOnly expectedDate,
        string expectedMerchant,
        decimal expectedAmount
    ) => AssertRow(DnbHeader, rawLine, expectedDate, expectedMerchant, expectedAmount);

    [Theory]
    [MemberData(nameof(ValleCases))]
    public void Valle_ParsesCorrectly(
        string rawLine,
        DateOnly expectedDate,
        string expectedMerchant,
        decimal expectedAmount
    ) => AssertRow(ValleHeader, rawLine, expectedDate, expectedMerchant, expectedAmount);

    private static void AssertRow(
        string header,
        string rawLine,
        DateOnly expectedDate,
        string expectedMerchant,
        decimal expectedAmount
    )
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(header + "\n" + rawLine);
        using var reader = new StreamReader(new MemoryStream(bytes));
        var rows = new CsvParser().Parse(reader);

        Assert.Single(rows);
        Assert.Equal(expectedDate, rows[0].Date);
        Assert.Equal(expectedMerchant, rows[0].Merchant);
        Assert.Equal(expectedAmount, rows[0].Amount);
    }
}
