public static class Test
{
    static Test()
    {
        var parser = new CsvParser();

        var stream = File.OpenRead("../../../../Personlig/data/kasper/transactions.txt");
        var reader = new StreamReader(stream);
        var rows = parser.ParseRows(reader);

        Console.WriteLine(rows[1]);
    }
}
