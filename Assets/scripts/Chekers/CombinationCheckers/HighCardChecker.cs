using System.Collections.Generic;
using System.Linq;

public class HighCardChecker : ICombinationChecker
{
    public string CombinationName => "HighCard";

    public CombinationResult Check(IReadOnlyList<Card> cards)
    {
        var tiebreakers = cards
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(5)
        .ToList();

        var result = new CombinationResult(CombinationCategory.HighCard, tiebreakers);
        return result;
    }
}
