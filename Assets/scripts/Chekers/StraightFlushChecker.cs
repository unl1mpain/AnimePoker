using System.Collections.Generic;
using System.Linq;

public class StraightFlushChecker : ICombinationChecker
{
    public string CombinationName => "StraightFlush";

    public CombinationResult Check(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);

        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        var suitedCards = cards
        .GroupBy(c => c.Suit)
        .FirstOrDefault(g => g.Count() >= 5);

        if (suitedCards == null)
            return null;

        var straightResult = new StraightChecker().Check(suitedCards.ToList(), new List<Card>());
        if (straightResult == null)
        {
            return null;
        }

        Rank high = straightResult.Tiebreakers[0];

        var category = high == Rank.Ace
            ? CombinationCategory.RoyalFlush
            : CombinationCategory.StraightFlush;

        return new CombinationResult(category, straightResult.Tiebreakers);

    }
}
