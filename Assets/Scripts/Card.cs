public enum Suit 
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

public struct Card
{
    private Suit suit;
    private Rank rank;

    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
    }

    public override string ToString()
    {
        string rankString;

        switch(rank)
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
}