using UnityEngine;
using TMPro;

public class CardAmount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardAmount;
    [SerializeField] private DeckGenerator deckGenerator;

    void OnEnable()
    {
        deckGenerator.OnCardAmountChanged += UpdateCardAmount;
    }

    void OnDisable()
    {
        deckGenerator.OnCardAmountChanged -= UpdateCardAmount;
    }

    void UpdateCardAmount(int remainingCards)
    {
        cardAmount.text = $"{remainingCards} cards";
    }

}
