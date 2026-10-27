using System.Collections.Generic;
using UnityEngine;


public class Deck
{
    private List<Card> cards = new List<Card>();
    #region whyhere
    private Suit[] suits = (Suit[])System.Enum.GetValues(typeof(Suit));
    private Rank[] ranks = (Rank[])System.Enum.GetValues(typeof(Rank));
    #endregion
    public int Count => cards.Count;

    public Deck()
    {
        Reset();
    }
    public void Reset()
    {
        cards.Clear();
        foreach (Suit suit in suits)
        {
            foreach (Rank rank in ranks)
            {
                cards.Add(new Card(suit, rank));
            }
        }

        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public Card Draw() 
    {
        Card top = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return top;
    }

    public List<Card> GetCards() => cards;
}
