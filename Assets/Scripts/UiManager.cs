using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI cardNum1;
    public TextMeshProUGUI cardNum2;
    public TextMeshProUGUI jokboPoint;
    [SerializeField] Deck deck;
    

    private void OnEnable()
    {
        deck.cardNumUi += UpdateCard;
        deck.testJokboPointUi += TotalJokboPoint;
    }

    private void OnDisable()
    {
        deck.cardNumUi -= UpdateCard;
        deck.testJokboPointUi -= TotalJokboPoint;
    }


    void UpdateCard(int cardNum, bool isGwang)
    {
        if(deck.myCard.Count == 0)
        {
            cardNum1.text = "";
            cardNum2.text = "";
        }
        else if (deck.myCard.Count == 1)
        {
            cardNum1.text = cardNum.ToString() + " + " + isGwang;
        }
        else if (deck.myCard.Count == 2)
        {
            cardNum2.text = cardNum.ToString() + " + " + isGwang;
        }
    }

    void TotalJokboPoint(int TestPoint)
    {
        jokboPoint.text = TestPoint.ToString();
    }
}
