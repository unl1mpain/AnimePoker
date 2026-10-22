using System.Collections.Generic;
using System.Linq;

public class FullHouseChecker: ICombinationChecker
{
    public string CombinationName => "FullHouse";

    public CombinationResult Check(IReadOnlyList<Card> cards)
    {
        var tripleRanks = cards
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 3)
        .Select(g => g.Key)
        .OrderByDescending(r => r)
        .ToList();

        if (tripleRanks.Count == 0)
            return null;

        if (tripleRanks.Count == 2)
        {
            var result = new List<Rank> { tripleRanks[0], tripleRanks[1] };
            return new CombinationResult(CombinationCategory.FullHouse, result);
        }

        var pairRanks = cards 
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 2)
        .Select(g => g.Key)
        .OrderByDescending(r => r)
        .ToList();

        if (pairRanks.Count == 0)
            return null;

        var tiebreakers = new List<Rank> { tripleRanks[0], pairRanks[0] };

        return new CombinationResult(CombinationCategory.FullHouse, tiebreakers);
    }
}
