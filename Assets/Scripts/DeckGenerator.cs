using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckGenerator : MonoBehaviour
{
    [SerializeField] private int numberOfDecks = 6;
    private List<string> upcomingCards = new List<string>();
    private string[] cards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    private string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
    
    public event Action<int> OnCardAmountChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numberOfDecks; i++)
        {
            foreach (var suit in suits)
            {
                foreach (var card in cards)
                {
                    upcomingCards.Add($"{card} of {suit}");
                }
            }
        }
        ShuffleList(upcomingCards);

        // Optionally, print the deck to the console
        for (int i = 0; i < upcomingCards.Count; i++)
        {
            Debug.Log($"{upcomingCards[i]} at index {i}");
        }

        OnCardAmountChanged?.Invoke(upcomingCards.Count);
    }

    void ShuffleList(List<string> list)
    {
        System.Random rng = new System.Random();
        int cardsLeftToShuffle = list.Count;
        while (cardsLeftToShuffle > 1)
        {
            cardsLeftToShuffle--;
            int randomIndex = rng.Next(cardsLeftToShuffle + 1);
            string valueAtRandomIndex = list[randomIndex];
            list[randomIndex] = list[cardsLeftToShuffle];
            list[cardsLeftToShuffle] = valueAtRandomIndex;
        }
    }

    public void DrawCard()
    {
        if (upcomingCards.Count > 0)
        {
            string drawnCard = upcomingCards[0];
            upcomingCards.RemoveAt(0);
            OnCardAmountChanged?.Invoke(upcomingCards.Count);
        }
    }
}
