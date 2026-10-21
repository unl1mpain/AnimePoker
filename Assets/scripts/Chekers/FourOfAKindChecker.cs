using System.Collections.Generic;
using System.Linq;

public class FourOfAKindChecker : ICombinationChecker
{
    public string CombinationName => "FourOfAKind";

    public CombinationResult Check(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);

        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        var quadRanks = cards
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 4)
        .Select(g => g.Key)
        .ToList();

        if (quadRanks.Count < 1)
            return null;

        var kickers = cards
        .Where(c => c.Rank != quadRanks[0])
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(1);

        var tiebreakers = new List<Rank> { quadRanks[0] };
        tiebreakers.AddRange(kickers);

        return new CombinationResult(CombinationCategory.FourOfAKind, tiebreakers);
    }
}