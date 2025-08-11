using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class DeckGenerator : MonoBehaviour
{
    [SerializeField] private static int numberOfDecks = 6;
    [SerializeField] private float dealSpeed = 0.5f;

    private List<string> upcomingCards = new List<string>();
    private static string[] cards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    private static string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

    public event Action<int> OnCardAmountChanged;
    [SerializeField] private GameObject playerCard0;
    [SerializeField] private GameObject playerCard1;
    [SerializeField] private GameObject playerCard2;
    [SerializeField] private GameObject dealerCard0;
    [SerializeField] private GameObject dealerCard1;

    private TMPro.TextMeshProUGUI playerCard0Text;
    private TMPro.TextMeshProUGUI playerCard1Text;
    private TMPro.TextMeshProUGUI playerCard2Text;
    private TMPro.TextMeshProUGUI dealerCard0Text;
    private TMPro.TextMeshProUGUI dealerCard1Text;

    private UnityEngine.UI.Image playerCard0Image;
    private UnityEngine.UI.Image playerCard1Image;
    private UnityEngine.UI.Image playerCard2Image;
    private UnityEngine.UI.Image dealerCard0Image;
    private UnityEngine.UI.Image dealerCard1Image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCard0Text = playerCard0.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        playerCard1Text = playerCard1.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        playerCard2Text = playerCard2.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        dealerCard0Text = dealerCard0.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        dealerCard1Text = dealerCard1.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        playerCard0Image = playerCard0.GetComponent<UnityEngine.UI.Image>();
        playerCard1Image = playerCard1.GetComponent<UnityEngine.UI.Image>();
        playerCard2Image = playerCard2.GetComponent<UnityEngine.UI.Image>();
        dealerCard0Image = dealerCard0.GetComponent<UnityEngine.UI.Image>();
        dealerCard1Image = dealerCard1.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < numberOfDecks; i++)
        {
            foreach (var suit in suits)
            {
                foreach (var card in cards)
                {
                    upcomingCards.Add($"{card} of\n {suit}");
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

    private string DrawCard()
    {
        if (upcomingCards.Count > 0)
        {
            string drawnCard = upcomingCards[0];
            upcomingCards.RemoveAt(0);
            OnCardAmountChanged?.Invoke(upcomingCards.Count);
            return drawnCard;
        }
        return null;
    }

    public void StartDealing()
    {
        StartCoroutine(Deal());
    }

    IEnumerator Deal()
    {
        if (upcomingCards.Count > 0)
        {
            //players 1st card
            playerCard0Text.text = DrawCard();
            playerCard0Image.color = Color.white;
            playerCard0Image.enabled = true;
            playerCard0Text.enabled = true;
            yield return new WaitForSeconds(dealSpeed);

            //dealer 1st card (hidden)
            dealerCard0Text.text = DrawCard();
            dealerCard0Image.enabled = true;
            yield return new WaitForSeconds(dealSpeed);

            //players 2nd card
            playerCard1Text.text = DrawCard();
            playerCard1Image.color = Color.white;
            playerCard1Image.enabled = true;
            playerCard1Text.enabled = true;
            yield return new WaitForSeconds(dealSpeed);

            //dealer 2nd card
            dealerCard1Text.text = DrawCard();
            dealerCard1Image.color = Color.white;
            dealerCard1Image.enabled = true;
            dealerCard1Text.enabled = true;
        }
    }

    public void Hit()
    {
        if (upcomingCards.Count > 0)
        {
            playerCard2Text.text = DrawCard();
            playerCard2Image.color = Color.white;
            playerCard2Image.enabled = true;
            playerCard2Text.enabled = true;
        }
        RevealDealerCard();
    }

    public void Stand()
    {
        RevealDealerCard();
    }

    private void RevealDealerCard()
    {
        dealerCard0Image.color = Color.white;
        dealerCard0Text.enabled = true;
        dealerCard0Image.enabled = true;
    }
}
