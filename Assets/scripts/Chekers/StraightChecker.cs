using System.Collections.Generic;
using System.Linq;

public class StraightChecker : ICombinationChecker
{
    public string CombinationName => "Straight";

    public CombinationResult Check(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);

        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        var sortedRanks = cards
        .Select(c => c.Rank)
        .Distinct()
        .OrderByDescending(r => r)
        .ToList();

        if (sortedRanks.Count < 5)
            return null;

        Rank? straightHigh = null;

        for (int i = 0; i <= sortedRanks.Count - 5; i++) 
        {
            if ((int)sortedRanks[i] - (int)sortedRanks[i + 4] == 4)    
            {
                straightHigh = sortedRanks[i];
                break;
            }
        }

        if (straightHigh == null && sortedRanks.Contains(Rank.Ace))
        {
            if ((int)sortedRanks[sortedRanks.Count - 1] == 2 &&
                (int)sortedRanks[sortedRanks.Count - 4] - (int)sortedRanks[sortedRanks.Count - 1] == 3)
            {
                straightHigh = Rank.Five;
            }
        }

        if (straightHigh == null)
            return null;

        return new CombinationResult(CombinationCategory.Straight, new List<Rank> { straightHigh.Value });
    }
}
