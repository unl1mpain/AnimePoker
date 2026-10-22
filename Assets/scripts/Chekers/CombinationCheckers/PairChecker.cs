using System.Collections.Generic;
using System.Linq;

public class PairChecker : ICombinationChecker
{
    public string CombinationName => "Pair";

    public CombinationResult Check(IReadOnlyList<Card> cards)
    {
        var pairRanks = cards 
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 2)
        .Select(g => g.Key)
        .ToList();

        if (pairRanks.Count == 0)
            return null;

        Rank pairRank = pairRanks.Max();

        var kickers = cards 
        .Where(c => c.Rank != pairRank)
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(3);

        var tiebreakers = new List<Rank> { pairRank };
        tiebreakers.AddRange(kickers);

        return new CombinationResult(CombinationCategory.Pair, tiebreakers);
    }
}