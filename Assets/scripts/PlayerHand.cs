using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHand : Hand
{
    public override void AddCard(Card card) 
    {
        if (hand.Count < 2) 
        {
            hand.Add(card);
        }
    }
}
