using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckGenerator : MonoBehaviour
{
    [SerializeField] public GameObject cardPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SettingsManager settingsManager;
    

    public event Action<int> OnCardAmountChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < settingsManager.numberOfDecks; i++)
        {
            foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
                {
                    GameObject generatedCard = Instantiate(cardPrefab, canvas.transform);
                    RectTransform cardRect = generatedCard.GetComponent<RectTransform>();
                    cardRect.anchorMin = new Vector2(0, 1);
                    cardRect.anchorMax = new Vector2(0, 1);
                    cardRect.anchoredPosition = new Vector2(100, -100);
                    generatedCard.name = $"{rank} of {suit}";
                    generatedCard.GetComponent<Card>().Setup(suit, rank);
                    gameManager.cardsInShoe.Add(generatedCard);
                }
            }
        }
        ShuffleList(gameManager.cardsInShoe);

        // DEBUG print the deck to the console
        for (int i = 0; i < gameManager.cardsInShoe.Count; i++)
        {
            Debug.Log($"{gameManager.cardsInShoe[i]} at index {i}");
        }
        OnCardAmountChanged?.Invoke(gameManager.cardsInShoe.Count);
    }

    void ShuffleList(List<GameObject> list)
    {
        System.Random rng = new System.Random();
        int cardsLeftToShuffle = list.Count;
        while (cardsLeftToShuffle > 1)
        {
            cardsLeftToShuffle--;
            int randomIndex = rng.Next(cardsLeftToShuffle + 1);
            GameObject valueAtRandomIndex = list[randomIndex];
            list[randomIndex] = list[cardsLeftToShuffle];
            list[cardsLeftToShuffle] = valueAtRandomIndex;
        }
    }
}
