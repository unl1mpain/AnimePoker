public class PlayerHand : Hand
{
    protected override int MaxCards => 2;
    public override void AddCard(Card card) 
    {
        if (!IsFull) return;
        hand.Add(card);
    }
}
