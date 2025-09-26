using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Suit suit { get; private set; }
    public Rank rank { get; private set; }

    [Header("Art References")]
    [SerializeField] private Sprite cardBackSprite;
    private Sprite cardFrontSprite;
    [SerializeField] private Sprite[] heartSprites;   // 13 sprites in order: Ace → King
    [SerializeField] private Sprite[] diamondSprites;
    [SerializeField] private Sprite[] clubSprites;
    [SerializeField] private Sprite[] spadeSprites;
    private Image image;

    private bool isFaceUp = false;

    void Awake()
    {
        image = GetComponent<Image>(); 
    }

    public void Setup(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
        UpdateArt();
    }

    void UpdateArt()
    {
        int index = (int)rank - 1;
        switch (suit)
        {
            case Suit.Hearts: cardFrontSprite = heartSprites[index]; break;
            case Suit.Diamonds: cardFrontSprite = diamondSprites[index]; break;
            case Suit.Clubs: cardFrontSprite = clubSprites[index]; break;
            case Suit.Spades: cardFrontSprite = spadeSprites[index]; break;
        }
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

    public int Value
    {
        get
        {
            if (rank == Rank.Jack || rank == Rank.Queen || rank == Rank.King)
                return 10;
            return (int)rank;
        }
    }

    public void FlipCard()
    {
        isFaceUp = !isFaceUp;
        image.sprite = isFaceUp ? cardFrontSprite : cardBackSprite;
    }
}
