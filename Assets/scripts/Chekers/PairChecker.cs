using System.Collections.Generic;
using System.Linq;

public class PairChecker : ICombinationChecker
{
    public string CombinationName => "Pair";

    public CombinationResult Check(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);
        
        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        var pairRanks = cards // list of pairs sorted by their count   
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 2)
        .Select(g => g.Key)
        .ToList();

        if (pairRanks.Count == 0)
            return null;

        Rank pairRank = pairRanks.Max();

        var kickers = cards // list of kickers ordered by descending
        .Where(c => c.Rank != pairRank)
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(3);

        var tiebreakers = new List<Rank> { pairRank };
        tiebreakers.AddRange(kickers);

        var result = new CombinationResult(CombinationCategory.Pair, tiebreakers);
        return result;
    }
}