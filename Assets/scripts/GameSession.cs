
public class GameSession
{
    private readonly Deck currentDeck;
    private readonly HandEvaluator evaluator = new HandEvaluator();
    public Hand Board { get; }
    public HoleCards PlayerHand { get; }
    public HoleCards EnemyHand { get; }

    public GameSession()
    {
        currentDeck = new Deck();
        Board = new Hand();
        PlayerHand = new HoleCards();
        EnemyHand = new HoleCards();

        NewDeal();
    }

    public void NewDeal() //первоначальная раздача
    {
        Board.ClearHand();
        PlayerHand.ClearHand();
        EnemyHand.ClearHand();
        currentDeck.Reset();

        PlayerHand.AddCard(currentDeck.Draw());
        PlayerHand.AddCard(currentDeck.Draw());
        EnemyHand.AddCard(currentDeck.Draw());
        EnemyHand.AddCard(currentDeck.Draw());
        Board.AddCard(currentDeck.Draw());
        Board.AddCard(currentDeck.Draw());
        Board.AddCard(currentDeck.Draw());
    }

    public void DrawCard() 
    {
        if (Board.IsFull) return;
        Board.AddCard(currentDeck.Draw());
    }

    public ShowdownResult ResolveShowdown()
    {
        var playerResult = evaluator.Evaluate(PlayerHand.GetHand(), Board.GetHand());
        var enemyResult = evaluator.Evaluate(EnemyHand.GetHand(), Board.GetHand());

        if (playerResult == null || enemyResult == null)
        {
            return null;
        }

        ResultType showdownResultType;
        var comparison = playerResult.CompareTo(enemyResult);
        if (comparison < 0)
        {
            showdownResultType = ResultType.EnemyWin;
        }
        else if (comparison > 0)
        {
            showdownResultType = ResultType.PlayerWin;
        }
        else
        {
            showdownResultType = ResultType.Tie;
        }

        return new ShowdownResult(playerResult, enemyResult, showdownResultType);
    }
}
