using System.Collections.Generic;
using System.Linq;

public class TwoPairChecker : ICombinationChecker
{
    public string CombinationName => "TwoPair";

    public CombinationResult Check(IReadOnlyList<Card> cards)
    {
        var pairRanks = cards
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 2)
        .Select(g => g.Key)
        .OrderByDescending(r => r)
        .ToList();

        if (pairRanks.Count < 2)
            return null;

        Rank highPair = pairRanks[0];
        Rank lowPair = pairRanks[1];

        Rank kicker = cards
            .Where(c => c.Rank != highPair && c.Rank != lowPair)
            .Select(c => c.Rank)
            .OrderByDescending(r => r)
            .First();

        var tiebreakers = new List<Rank> { highPair, lowPair, kicker };

        return new CombinationResult(CombinationCategory.TwoPair, tiebreakers);
    }
}
