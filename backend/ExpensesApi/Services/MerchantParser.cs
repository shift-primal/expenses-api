using System.Text.RegularExpressions;

public static class MerchantParser
{
    public static string ParseDnb(string raw)
    {
        var visaNew = Regex.Match(raw, @"^Visa\s+\d+\s{2,}(?:[A-Za-z]{2,3}\s+[\d,]+\s+)?(.+)$");
        if (visaNew.Success)
        {
            var m = visaNew.Groups[1].Value.Trim();
            m = Regex.Replace(m, @"\s{2,}.*$", "");
            m = Regex.Replace(m, @"\s+Valutakurs:.*$", "");
            m = Regex.Replace(m, @"::\S+", "");
            m = Regex.Replace(m, @"\s+\d+$", "");
            return m.Trim();
        }

        var visa = Regex.Match(raw, @"^Visa \d+ (?:Nok \d+,\d+ )?(.+?)(?:\s+\d+)?$");
        if (visa.Success)
            return visa.Groups[1].Value;

        var varekjop = Regex.Match(raw, @"^(?:E-)?[Vv]arekjøp\s+(.+?)\s+(?=\d|Dato)");
        if (varekjop.Success)
            return varekjop.Groups[1].Value;

        var overforing = Regex.Match(raw, @"^Overføring\s+(?:\S+\s+)?\d+\s+(.+)$");
        if (overforing.Success)
        {
            var entity = overforing.Groups[1].Value;
            entity = Regex.Replace(entity, @"\s+-\s+.*$", "");
            var caps = Regex.Match(entity, @"^[A-ZÆØÅ]{2,}\b");
            if (caps.Success)
                return $"Overføring - {caps.Value}";

            var name = Regex.Match(entity, @"^\p{Lu}\p{Ll}+(?:\s+\S+)?");
            if (name.Success)
                return $"Overføring - {name.Value.Trim()}";

            return $"Overføring - {entity.Trim()}";
        }

        var giro = Regex.Match(raw, @"^Giro\s+\d+\s+(\p{Lu}\p{Ll}+\s+\p{Lu}\p{Ll}+)");
        if (giro.Success)
            return $"Giro - {giro.Groups[1].Value}";

        var konto = Regex.Match(raw, @"^Kontoregulering\s+\d+\s+(\S+)");
        if (konto.Success)
            return $"Kontoregulering - {konto.Groups[1].Value}";

        return raw.Trim();
    }

    public static string ParseValle(string raw)
    {
        var vipps = Regex.Match(raw, @"^Vipps\*(.+)");
        if (vipps.Success)
            return vipps.Groups[1].Value.Trim();

        var datePrefix = Regex.Match(raw, @"^\d{2}\.\d{2}\s+(.+)");
        if (datePrefix.Success)
        {
            var rest = datePrefix.Groups[1].Value;
            var m = Regex.Match(
                rest,
                @"^(.+?)(?:\s+AVD\b|\s+\d{3,}|\s+[A-ZÆØÅ]\d{3,}|\s+[A-ZÆØÅ]{2,4}\s+\S+(?:GATE|GATA|VEG|VN|SVIN|GT)\S*|\s+\S+(?:GATE|GATA|VEG|VN|SVIN|GT)\S*|$)"
            );
            return m.Groups[1].Value.Trim();
        }

        var terminal = Regex.Match(raw, @"^\d+\s+(.+)");
        if (terminal.Success)
            return terminal.Groups[1].Value.Trim();

        return Regex.Replace(raw, @"\s*\(\d[\d\s]*\)\s*$", "").Trim();
    }
}
