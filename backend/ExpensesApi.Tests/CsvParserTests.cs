public class CsvParserTests
{
    public static TheoryData<string, string> Cases =>
        new()
        {
            { "Visa 100022 Google One", "Google One" },
            { "Visa 100034 Easypark Norge", "Easypark Norge" },
            { "Visa 100022 Nok 109,00 Paypal :discord", "Paypal" },
            { "Visa 100022 Nok 149,00 Netflix", "Netflix" },
            { "Varekjøp Normal Avd 12 Storgata Gjøvik Dato 02.02 kl. 10.54", "Normal" },
            {
                "Overføring Innland 9923 Marte Solbakken Hagen Tilbakebetaling mat",
                "Marte Solbakken Hagen"
            },
            { "Varekjøp i butikk SPAR BRUGATA F Reservert transaksjon", "SPAR" },
            { "Varekjøp i butikk JOKER MOELV F Reservert transaksjon", "JOKER" },
            { "Kontoregulering 819 Bjørg", "Bjørg" },
            { "Visa 100034 Burger King 5041", "Burger King" },
            { "Varekjøp i butikk REMA 1000 TORGET F Reservert transaksjon", "REMA 1000" },
            { "Overføring Innland 4456 Ragnhild Gjerde Berge Pakkepost", "Ragnhild Gjerde Berge" },
        };

    [Theory]
    [MemberData(nameof(Cases))]
    public void GetMerchant_ReturnsExpectedMerchant(string description, string expected)
    {
        var result = CsvParser.GetMerchant(description);
        Assert.Equal(expected, result);
    }
}
