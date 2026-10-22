using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class FlushChecker : ICombinationChecker
{
    public string CombinationName => "Flush";

    public CombinationResult Check(IReadOnlyList<Card> cards)
    {
        var suitedCards = cards
        .GroupBy(c => c.Suit)
        .FirstOrDefault(g => g.Count() >= 5);

        if (suitedCards == null)
            return null;


        var topRanks = suitedCards
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(5);

        var tiebreakers = new List<Rank>(topRanks);

        return new CombinationResult(CombinationCategory.Flush, tiebreakers);
    }
}