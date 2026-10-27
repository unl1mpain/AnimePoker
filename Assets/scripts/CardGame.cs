using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CardGame : MonoBehaviour
{
    [SerializeField] private TMP_Text outputCurrentHand;
    [SerializeField] private TMP_Text outputPlayerHand;
    [SerializeField] private TMP_Text outputEnemyHand;
    private Deck currentDeck;
    private Hand gameHand;
    private PlayerHand playerHand;
    private PlayerHand enemyHand;

    private void Start()
    {
        currentDeck = new Deck();
        gameHand = new Hand();
        playerHand = new PlayerHand();
        enemyHand = new PlayerHand();

        NewDeal(currentDeck, gameHand, playerHand, enemyHand);
        outputCurrentHand.text = gameHand.ShowHand();
        outputPlayerHand.text = playerHand.ShowHand();

        //TestThreePairs();
    }

    private void Update()
    {
        TextUpdate();

        if (Keyboard.current.eKey.wasPressedThisFrame && !gameHand.IsFull) // draw
        {
            gameHand.AddCard(currentDeck.Draw());
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)  // очистка партии и старт новой
        {
            
            NewDeal(currentDeck, gameHand, playerHand, enemyHand);
        }

        if (Keyboard.current.hKey.wasPressedThisFrame) // проверка комбинации
        {
            CheckCombination();
        }
    }

    private void NewDeal(Deck deck, Hand gameHand, PlayerHand playerHand, PlayerHand enemyHand) //первоначальная раздача
    {
        gameHand.ClearHand();
        playerHand.ClearHand();
        enemyHand.ClearHand();
        currentDeck.Reset();

        playerHand.AddCard(deck.Draw());
        playerHand.AddCard(deck.Draw());
        enemyHand.AddCard(deck.Draw());
        enemyHand.AddCard(deck.Draw());
        gameHand.AddCard(deck.Draw());
        gameHand.AddCard(deck.Draw());
        gameHand.AddCard(deck.Draw());
    }

    private void TextUpdate() 
    {
        outputCurrentHand.text = gameHand.ShowHand();
        outputPlayerHand.text = playerHand.ShowHand();
        outputEnemyHand.text = enemyHand.ShowHand();
    }

    private void CheckCombination()
    {
        var evaluator = new HandEvaluator();
        var result1 = evaluator.Evaluate(playerHand.GetHand(), gameHand.GetHand());

        var result2 = evaluator.Evaluate(enemyHand.GetHand(), gameHand.GetHand());
        if (result1 == null || result2 == null)
        {
            Debug.Log("Нет комбинации");
            return;
        }
        Debug.Log("Your combination:");
        Debug.Log(result1.Category);
        Debug.Log(string.Join(", ", result1.Tiebreakers));

        Debug.Log("Enemy combination:");
        Debug.Log(result2.Category);
        Debug.Log(string.Join(", ", result2.Tiebreakers));

        Debug.Log("Winner is:");
        var a = result1.CompareTo(result2);
        if (a < 0)
        {
            Debug.Log("enemy :(");
        }
        else if (a > 0)
        {
            Debug.Log("YOUUU!!!!!!");
        }
        else 
        {
            Debug.Log("WTF EQUAL");
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
