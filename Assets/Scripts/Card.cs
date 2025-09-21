using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Suit suit;
    private Rank rank;
    private TMPro.TextMeshProUGUI Text;
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite cardFrontSprite;

    private bool isFaceUp = false;

    public void Setup(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
        Text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Text.text = ToString();
    }

    public override string ToString()
    {
        string rankString;

        switch (rank)
        {
            case Rank.Ace:
            case Rank.Jack:
            case Rank.Queen:
            case Rank.King:
                rankString = rank.ToString();
                break;
            default:
                rankString = ((int)rank).ToString();
                break;
        }
        return $"{rankString} of\n {suit}";
    }

    public void FlipCard()
    {
        isFaceUp = !isFaceUp;
        GetComponent<UnityEngine.UI.Image>().sprite = isFaceUp ? cardFrontSprite : cardBackSprite;
        Text.enabled = isFaceUp;
    }
}
