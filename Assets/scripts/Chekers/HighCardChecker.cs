using System.Collections.Generic;
using System.Linq;

public class HighCardChecker : ICombinationChecker
{
    public string CombinationName => "HighCard";

    public CombinationResult Check(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);

        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        var tiebreakers = cards // list of kickers ordered by 
        .Select(c => c.Rank)
        .OrderByDescending(r => r)
        .Take(5)
        .ToList();

        var result = new CombinationResult(CombinationCategory.HighCard, tiebreakers);
        return result;
    }
}
