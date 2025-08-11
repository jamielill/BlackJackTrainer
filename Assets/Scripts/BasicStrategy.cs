using UnityEngine;


public class BasicStrategy : MonoBehaviour
{
    [SerializeField] private GameObject dealerCard1;
    private TMPro.TextMeshProUGUI dealerCard1Text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dealerCard1Text = dealerCard1.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckStrategy()
    {
        // cases for dealers upcard assuming no double cards
        switch (dealerCard1Text.text)
        {
            case "A":
            {
                // Implement basic strategy logic for Ace
                break;
            }
            case "K":
            case "Q":
            case "J":
            case "10":
            {
                // Implement basic strategy logic for King, Queen, Jack, and 10
                break;
            }
            case "9":
            {
                // Implement basic strategy logic for 9
                break;
            }
            case "8":
            {
                // Implement basic strategy logic for 8
                break;
            }
            case "7":
            {
                // Implement basic strategy logic for 7
                break;
            }
            case "6":
            {
                // Implement basic strategy logic for 6
                break;
            }
            case "5":
            {
                // Implement basic strategy logic for 5
                break;
            }
            case "4":
            {
                // Implement basic strategy logic for 4
                break;
            }
            case "3":
            {
                // Implement basic strategy logic for 3
                break;
            }
            case "2":
            {
                // Implement basic strategy logic for 2
                break;
            }
        }
    }
}
