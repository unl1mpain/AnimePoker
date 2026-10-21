using System.Collections.Generic;
using System.Linq;

public class ThreeOfAKindChecker : ICombinationChecker
{
    public string CombinationName => "ThreeOfAKind";

    public CombinationResult Check(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);

        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        var tripleRanks = cards
        .GroupBy(c => c.Rank)
        .Where(g => g.Count() == 3)
        .Select(g => g.Key)
        .OrderByDescending(r => r)
        .ToList();

        if (tripleRanks.Count < 1)
            return null;

        var kickers = cards 
        .Where(c => c.Rank != tripleRanks[0])
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(2);

        var tiebreakers = new List<Rank> { tripleRanks[0] };
        tiebreakers.AddRange(kickers);

        return new CombinationResult(CombinationCategory.ThreeOfAKind, tiebreakers);
    }
}
