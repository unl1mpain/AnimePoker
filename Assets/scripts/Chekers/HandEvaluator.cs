using System.Collections.Generic;


public class HandEvaluator
{
    private List<ICombinationChecker> CombinationsList = new()
    {
        new StraightFlushChecker(),
        new FourOfAKindChecker(),
        new FullHouseChecker(),
        new FlushChecker(),
        new StraightChecker(),
        new ThreeOfAKindChecker(),
        new TwoPairChecker(),
        new PairChecker(),
        new HighCardChecker()
    };

    public CombinationResult Evaluate(List<Card> hand, List<Card> table)
    {
        List<Card> cards = new List<Card>(table);
        foreach (Card card in hand)
        {
            cards.Add(card);
        }

        CombinationResult result;
        foreach (ICombinationChecker checker in CombinationsList)
        {
            result = checker.Check(cards);
            if (result != null)
                return result;
        }

        return null;
    }
}
