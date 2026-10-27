using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CardGame : MonoBehaviour
{
    [SerializeField] private TMP_Text outputCurrentHand;
    [SerializeField] private TMP_Text outputPlayerHand;
    [SerializeField] private TMP_Text outputEnemyHand;

    private GameSession gameSession;

    private void Start()
    {
        gameSession = new GameSession();



        //TestThreePairs();
    }

    private void Update()
    {
        TextUpdate();

        if (Keyboard.current.eKey.wasPressedThisFrame) // draw
        {
            gameSession.DrawCard();        
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)  // очистка партии и старт новой
        {
            gameSession.NewDeal();
        }

        if (Keyboard.current.hKey.wasPressedThisFrame) // проверка комбинации
        {
            ShowdownOutput();
        }
    }

    private void TextUpdate() 
    {
        outputCurrentHand.text = gameSession.Board.ShowHand();
        outputPlayerHand.text = gameSession.PlayerHand.ShowHand();
        outputEnemyHand.text = gameSession.EnemyHand.ShowHand();
    }

    private void ShowdownOutput()
    {
        var showdownResult = gameSession.ResolveShowdown();
        if (showdownResult == null)
        {
            Debug.Log("Нет комбинации");
            return;
        }
        Debug.Log("Your combination:");
        Debug.Log(showdownResult.PlayerResult.Category);
        Debug.Log(string.Join(", ", showdownResult.PlayerResult.Tiebreakers));

        Debug.Log("Enemy combination:");
        Debug.Log(showdownResult.EnemyResult.Category);
        Debug.Log(string.Join(", ", showdownResult.EnemyResult.Tiebreakers));

        Debug.Log("Winner is:");
        switch (showdownResult.Outcome)
        {
            case ResultType.PlayerWin:
                Debug.Log("YOUUU!!!!!!");
                break;
            case ResultType.EnemyWin:
                Debug.Log("enemy :(");
                break;
            case ResultType.Tie:
                Debug.Log("WTF EQUAL");
                break;
        }
    }

    private void TestThreePairs()
    {
        var cards = new List<Card>
        {
            new Card(Suit.Hearts, Rank.Ace),
            new Card(Suit.Hearts, Rank.King),
            new Card(Suit.Clubs, Rank.Queen),
            new Card(Suit.Hearts, Rank.Jack),
            new Card(Suit.Hearts, Rank.Ten),
            new Card(Suit.Spades, Rank.Three),
            new Card(Suit.Spades, Rank.Two)
        };

        var result = new StraightFlushChecker().Check(cards);
        Debug.Log(result == null ? "null" : $"{result.Category}: {string.Join(", ", result.Tiebreakers)}");
    }
}
