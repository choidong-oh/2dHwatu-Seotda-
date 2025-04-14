using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI cardNum1;
    public TextMeshProUGUI cardNum2;
    public TextMeshProUGUI jokboPoint;
    public TextMeshProUGUI AiJokboPoint;
    public TextMeshProUGUI Winner;

    public TextMeshProUGUI PlayerMoney;
    [SerializeField] Deck deck;

    private void Update()
    {
        PlayerMoney.text = "PlayerMoney : " + Player.PlayerMoney.ToString();


    }
    private void OnEnable()
    {
        deck.cardNumUi += UpdateCard;
    }

    private void OnDisable()
    {
        deck.cardNumUi -= UpdateCard;
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

    public void TotalJokboPoint(int TestPoint)
    {
        jokboPoint.text = TestPoint.ToString();
    }
    public void AiTotalJokboPoint(int TestPoint)
    {
        AiJokboPoint.text = TestPoint.ToString();
    }
    public void WhoWinner(int playerPoint, int aiPoint)
    {
        string winner = playerPoint > aiPoint ? "PlayerWin" : (playerPoint < aiPoint ? "AiWin" : "Draw");

        Winner.text = winner.ToString();
    }

}
