public class ShowdownResult
{
    public CombinationResult PlayerResult { get; private set; }
    public CombinationResult EnemyResult { get; private set; }
    public ResultType Outcome { get; private set; }

    public ShowdownResult(CombinationResult playerResult, CombinationResult enemyResult, ResultType resultType)
    {
        PlayerResult = playerResult;
        EnemyResult = enemyResult;
        Outcome = resultType;
    }
}

public enum ResultType
{
    Tie,
    PlayerWin,
    EnemyWin
}
