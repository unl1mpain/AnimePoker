using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CardGame : MonoBehaviour
{
    [SerializeField] private TMP_Text outputCurrentHand;
    [SerializeField] private TMP_Text outputPlayerHand;
    private Deck currentDeck;
    private Hand gameHand;
    private PlayerHand playerHand;

    private void Start()
    {
        currentDeck = new Deck();
        gameHand = new Hand();
        playerHand = new PlayerHand();

        StartDeal(currentDeck, gameHand, playerHand);
        outputCurrentHand.text = gameHand.ShowHand();
        outputPlayerHand.text = playerHand.ShowHand();

        //Debug.Log(playerHand.ToSafeString());
        //Debug.Log(gameHand.ShowHand());
        TestThreePairs();
    }

    private void Update()
    {
        TextUpdate();

        if (Keyboard.current.eKey.wasPressedThisFrame) // draw
        {
            gameHand.AddCard(currentDeck.Draw());
            
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)  // очистка партии
        {
            gameHand.ClearHand();
            playerHand.ClearHand();
           
        }

        if (Keyboard.current.gKey.wasPressedThisFrame) // начало новой
        {
            currentDeck.Reset();
            StartDeal(currentDeck, gameHand, playerHand);
            
        }

        if (Keyboard.current.hKey.wasPressedThisFrame) // проверка комбинации
        {
            CheckCombination();
        }
    }

    private void StartDeal(Deck deck, Hand gameHand, PlayerHand playerHand) //первоначальная раздача
    {
        playerHand.AddCard(deck.Draw());
        playerHand.AddCard(deck.Draw());
        gameHand.AddCard(deck.Draw());
        gameHand.AddCard(deck.Draw());
        gameHand.AddCard(deck.Draw());
    }

    private void TextUpdate() 
    {
        outputCurrentHand.text = gameHand.ShowHand();
        outputPlayerHand.text = playerHand.ShowHand();
    }

    private void CheckCombination()
    {
        var checker = new HighCardChecker();
        var result = checker.Check(playerHand.GetHand(), gameHand.GetHand());

        if (result == null)
        {
            Debug.Log("Нет комбинации");
            return;
        }

        Debug.Log(result.Category);
        Debug.Log(string.Join(", ", result.Tiebreakers));
    }

    private void TestThreePairs()
    {
        var hand = new List<Card>
        {
            new Card(Suit.Spades, Rank.Ace),
            new Card(Suit.Spades, Rank.King)
        };

        var table = new List<Card>
        {
            new Card(Suit.Clubs, Rank.Nine),
            new Card(Suit.Hearts, Rank.Five),
            new Card(Suit.Hearts, Rank.Four),
            new Card(Suit.Hearts, Rank.Three),
            new Card(Suit.Hearts, Rank.Two)
        };

        var result = new FlushChecker().Check(hand, table);
        Debug.Log(result == null ? "null" : $"{result.Category}: {string.Join(", ", result.Tiebreakers)}");
    }
}
