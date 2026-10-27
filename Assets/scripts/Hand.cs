using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

public class Hand
{
    protected List<Card> hand = new List<Card>();
    public bool IsFull => hand.Count >= 5;

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
