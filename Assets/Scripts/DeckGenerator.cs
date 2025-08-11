using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckGenerator : MonoBehaviour
{
    [SerializeField] private int numberOfDecks = 6;
    [SerializeField] private float dealSpeed = 0.5f;
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite cardFrontSprite;

    private List<Card> upcomingCards = new List<Card>();

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
            foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
                {
                    upcomingCards.Add(new Card(suit, rank));
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

    void ShuffleList(List<Card> list)
    {
        System.Random rng = new System.Random();
        int cardsLeftToShuffle = list.Count;
        while (cardsLeftToShuffle > 1)
        {
            cardsLeftToShuffle--;
            int randomIndex = rng.Next(cardsLeftToShuffle + 1);
            Card valueAtRandomIndex = list[randomIndex];
            list[randomIndex] = list[cardsLeftToShuffle];
            list[cardsLeftToShuffle] = valueAtRandomIndex;
        }
    }

    private string DrawCard()
    {
        if (upcomingCards.Count > 0)
        {
            Card drawnCard = upcomingCards[0];
            upcomingCards.RemoveAt(0);
            OnCardAmountChanged?.Invoke(upcomingCards.Count);
            return drawnCard.ToString();
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
            playerCard0Image.sprite = cardFrontSprite;
            playerCard0Image.enabled = true;
            playerCard0Text.enabled = true;
            yield return new WaitForSeconds(dealSpeed);

            //dealer 1st card (hidden)
            dealerCard0Text.text = DrawCard();
            dealerCard0Image.enabled = true;
            yield return new WaitForSeconds(dealSpeed);

            //players 2nd card
            playerCard1Text.text = DrawCard();
            playerCard1Image.sprite = cardFrontSprite;
            playerCard1Image.enabled = true;
            playerCard1Text.enabled = true;
            yield return new WaitForSeconds(dealSpeed);

            //dealer 2nd card
            dealerCard1Text.text = DrawCard();
            dealerCard1Image.sprite = cardFrontSprite;
            dealerCard1Image.enabled = true;
            dealerCard1Text.enabled = true;
        }
    }

    public void Hit()
    {
        if (upcomingCards.Count > 0)
        {
            playerCard2Text.text = DrawCard();
            playerCard2Image.sprite = cardFrontSprite;
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
        dealerCard0Image.sprite = cardFrontSprite;
        dealerCard0Text.enabled = true;
        dealerCard0Image.enabled = true;
    }
}
