public class CsvParserTests
{
    public static TheoryData<string, string> Cases =>
        new()
        {
            {
                "Varekjøp Ikea Ringsaker Krokstadvege Furnes Dato 29.12 kl. 16.01 ",
                "Ikea Ringsaker Krokstadvege Furnes"
            },
            { "Visa 100034 Burger King 5041", "Burger King" },
            { "Kontoregulering  801 Cæsh  ", "Kontoregulering - Cæsh" },
            {
                "Overføring Innland  802 Sofie Krukhaug Linde Betaling Tpp: Vipps Mobilepay AS",
                "Overføring - Sofie Krukhaug"
            },
            { "Visa  100022  Nok 271,84 Paypal :riotgamesl ", "Paypal :riotgamesl" },
            { "E-varekjøp Kiwi 012 Raufos Raufoss Dato 17.11 kl. 21.26 ", "Kiwi" },
            { "Visa  100022  Nok 1200,00 Revolut::1154: ", "Revolut" },
            { "Visa  100121  Mcdonalds 57800048 ", "Mcdonalds" },
            { "Visa  100022  Nok 39,00 Apple.com/bill ", "Apple.com/bill" },
            { "Varekjøp Kiwi 207 Kapp Mjøsvegen 51 Kapp Dato 12.06 kl. 10.53 ", "Kiwi" },
            {
                "Overføring 4020805656 Isak Krukhaug Linde Tpp: Vipps Mobilepay AS",
                "Overføring - Isak Krukhaug"
            },
            { "Crown Seaways Valutakurs: 1,6147", "Crown Seaways" },
        };

    [Theory]
    [MemberData(nameof(Cases))]
    public void GetMerchant_ReturnsExpectedMerchant(string description, string expected)
    {
        var result = MerchantParser.Parse(description);
        Assert.Equal(expected, result);
    }
}
