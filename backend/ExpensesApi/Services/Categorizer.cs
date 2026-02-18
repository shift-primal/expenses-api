public class Categorizer
{
    private readonly List<CategoryRule> _rules;

    public Categorizer(ExpensesDb db)
    {
        _rules = db.CategoryRules.ToList();
    }

    public int Categorize(string description)
    {
        foreach (var r in _rules)
        {
            if (description.Contains(r.Keyword, StringComparison.OrdinalIgnoreCase))
                return r.CategoryId;
        }

        return 1800;
    }
}
