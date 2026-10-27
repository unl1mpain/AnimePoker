using System.Collections.Generic;
using System.Text;

public class Hand
{
    protected List<Card> hand = new List<Card>();
    protected virtual int MaxCards => 5;
    public bool IsFull => hand.Count >= MaxCards;

    public virtual void AddCard(Card card)
    {
        if (hand.Count < 5)
            hand.Add(card);
    }

    public void ClearHand() 
    {
        hand.Clear();
    }

    public string ShowHand() 
    {
        var sb = new StringBuilder();
        foreach (Card card in hand)
        {
            sb.AppendLine(card.ToString());
        }
        return sb.ToString();
    }

    public List<Card> GetHand() => hand;
}
