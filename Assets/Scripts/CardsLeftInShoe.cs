using UnityEngine;
using TMPro;

public class CardsLeftInShoe : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardAmount;
    [SerializeField] private DeckGenerator deckGenerator;
    [SerializeField] private GameManager gameManager;

    void OnEnable()
    {
        deckGenerator.OnCardAmountChanged += UpdateCardAmount;
        gameManager.OnCardAmountChanged += UpdateCardAmount;
    }

    void OnDisable()
    {
        deckGenerator.OnCardAmountChanged -= UpdateCardAmount;
        gameManager.OnCardAmountChanged -= UpdateCardAmount;
    }

    void UpdateCardAmount(int remainingCards)
    {
        cardAmount.text = $"{remainingCards} cards";
    }

}
