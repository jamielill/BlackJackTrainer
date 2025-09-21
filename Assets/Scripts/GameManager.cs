using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private Card card;
    [SerializeField] private UnityEngine.UI.Button hitButton;
    [SerializeField] private UnityEngine.UI.Button standButton;

    public List<GameObject> cardsInShoe = new List<GameObject>();
    public List<GameObject> playerCards = new List<GameObject>();
    public List<GameObject> dealerCards = new List<GameObject>();
    public List<GameObject> discardPile = new List<GameObject>();

    public event Action<int> OnCardAmountChanged;

    private int playerTotal = 0;
    private int dealerTotal = 0;

    private int correctPlays = 0;
    private int incorrectPlays = 0;
    private int totalPlays = 0;

    private GameObject DrawCard()
    {
        if (cardsInShoe.Count > 0)
        {
            GameObject drawnCard = cardsInShoe[0];
            cardsInShoe.RemoveAt(0);
            Debug.Log(OnCardAmountChanged);
            OnCardAmountChanged?.Invoke(cardsInShoe.Count);
            return drawnCard;
        }
        return null;
    }

    public void StartDealing()
    {
        StartCoroutine(Deal());

    }

    //deals player and dealer 2 cards each
    private IEnumerator Deal()
    {
        if (cardsInShoe.Count > 3)
        {
            for(int i = 0; i < 2; i++)
            {
                playerCards.Add(DrawCard());
                playerTotal += playerCards[playerCards.Count - 1].GetComponent<Card>().Value;
                Debug.Log("player total: " + playerTotal);
                playerCards[i].GetComponent<Card>().FlipCard();
                RectTransform cardRect = playerCards[i].GetComponent<RectTransform>();
                cardRect.anchorMin = new Vector2(0.5f, 0);
                cardRect.anchorMax = new Vector2(0.5f, 0);
                cardRect.anchoredPosition = new Vector2(-65+130*i, 100);
                yield return new WaitForSeconds(settingsManager.dealSpeed);

                dealerCards.Add(DrawCard());
                dealerTotal += dealerCards[playerCards.Count - 1].GetComponent<Card>().Value;
                Debug.Log("dealer total: " + dealerTotal);
                if (i == 1)
                {
                    dealerCards[i].GetComponent<Card>().FlipCard();
                }
                cardRect = dealerCards[i].GetComponent<RectTransform>();
                cardRect.anchorMin = new Vector2(0.5f, 1);
                cardRect.anchorMax = new Vector2(0.5f, 1);
                cardRect.anchoredPosition = new Vector2(-65+130*i, -100);
                yield return new WaitForSeconds(settingsManager.dealSpeed);
            }
            enableButtons();
        }
    }

    public void Hit()
    {
        if (cardsInShoe.Count > 0)
        {
            playerCards.Add(DrawCard());
            GameObject newCard = playerCards[playerCards.Count - 1];

            playerTotal += newCard.GetComponent<Card>().Value;
            Debug.Log("player total: " + playerTotal);
            newCard.GetComponent<Card>().FlipCard();
            RectTransform cardRect = newCard.GetComponent<RectTransform>();
            cardRect.anchorMin = new Vector2(0.5f, 0);
            cardRect.anchorMax = new Vector2(0.5f, 0);
            cardRect.anchoredPosition = new Vector2(-65 + 130 * (playerCards.Count - 1), 100);
        }

        CheckBust();
        if (settingsManager.OneHit || CheckBust())
        {
            hitButton.interactable = false;
            standButton.interactable = false;
            RevealDealerCard();
            StartCoroutine(DiscardHands());
        }     
    }

    public void Stand()
    {
        RevealDealerCard();
        //make dealer hit until 17 or higher
        hitButton.interactable = false;
        standButton.interactable = false;
        RevealDealerCard();
        StartCoroutine(DiscardHands());
    }

    private void RevealDealerCard()
    {
        dealerCards[0].GetComponent<Card>().FlipCard();
    }

    private bool CheckBust()
    {
        if (playerTotal > 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator DiscardHands()
    {
        yield return new WaitForSeconds(settingsManager.dealSpeed);
        foreach (GameObject card in playerCards)
        {
            discardPile.Add(card);
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.anchorMin = new Vector2(1, 1);
            cardRect.anchorMax = new Vector2(1, 1);
            cardRect.anchoredPosition = new Vector2(-100, -100);
        }
        playerCards.Clear();
        playerTotal = 0;

        foreach (GameObject card in dealerCards)
        {
            discardPile.Add(card);
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.anchorMin = new Vector2(1, 1);
            cardRect.anchorMax = new Vector2(1, 1);
            cardRect.anchoredPosition = new Vector2(-100, -100);
        }
        dealerCards.Clear();
        dealerTotal = 0;
        yield return new WaitForSeconds(settingsManager.dealSpeed);
        StartDealing();
    }

    private void enableButtons()
    {
        hitButton.interactable = true;
        standButton.interactable = true;
    }
}
