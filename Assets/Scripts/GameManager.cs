using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//4. 승패판결 string > enum으로 
//5. ai로직
//6. 족보
//3. json으로 playermoney static개념으로
//7. 밸런스 조정
//8. mainpot을 스태틱으로 start에서 ai한테 주고시작, 나갓을때 방지
//9. 게임메뉴 씬, 충전할수잇게
//10. ui자리배치
public class GameManager : MonoBehaviour
{
    //기본배팅 > 카드분배 > 배팅 > 카드분배 > 배팅 > 승판결
    [SerializeField] BettingSystem bettingSystem;
    [SerializeField] Deck deck;
    [SerializeField] WinnerSystem winnerSystem;
    [SerializeField] string aiBettingName;

    [SerializeField] GameObject bettingBtn;

    bool isDraw;



    //기본배팅버튼 > 셔플,카드드로우 > 배팅 활성화 > 카드드로우 > 배팅활성화 > 승판결
    private void Start()
    {
        //처음 ui 비활성화
        bettingSystem.UiInteractableFalse();
        bettingSystem.BettingCount = 0;
    }

    public void BaseBetting()
    {
        bettingSystem.ResetBetting();
        bettingSystem.BaseBetting(ref bettingSystem.playerMoney);
        bettingSystem.BaseBetting(ref bettingSystem.aiMoney);
        bettingBtn.SetActive(false);

        //셔플,카드드로우
        DeckShuffle();
        CardDraw();
    }

    void DrawBetting()
    {
        bettingBtn.SetActive(false);

        //셔플,카드드로우
        DeckShuffle();
        CardDraw();
    }

    public void DeckShuffle()
    {
        deck.DeckShuffle();
    }

    public void CardDraw()
    {
        deck.PlayerSpawnCardBtn();
        deck.AiSpawnCardBtn();

        //배팅활성화
        bettingSystem.AllInTrue();
    }

    public void Betting(string bettingName)
    {
        bettingSystem.PlayerBetting(bettingName);
        //플레이어가 다이한거임
        if(bettingName == "Die")
        {
            bettingSystem.isDie = false;
            bettingSystem.aiMoney += bettingSystem.mainPot;
            ResetBtn();
            return;
        }
        bettingSystem.AiBetting(aiBettingName);
        //AI가 다이한거임
        if (aiBettingName == "Die")
        {
            bettingSystem.isDie = false;
            bettingSystem.playerMoney += bettingSystem.mainPot;
            ResetBtn();
            return;
        }
        Debug.Log("dsds");


        //카드드로우
        if (bettingSystem.isSecondBet == false)
        {
            bettingSystem.isSecondBet = true;
            bettingSystem.isFirstBet = false;
            CardDraw();
        }

        bettingSystem.BettingCount++;

        //두번 배팅하면 승판결로
        if(bettingSystem.BettingCount >= 2)
        {
            bettingSystem.UiInteractableFalse();

            //승판결 이긴쪽 스케일 키우고
            Winner();

            //버튼 비활성화
            Invoke("ResetBtn", 2f);
        }
    }

    public void Winner()
    {
        int winnerResult = winnerSystem.Winner();

        List<Card> winnerCards = null;
        if (winnerResult == 0)
        {
            winnerCards = deck.myCard;

            bettingSystem.playerMoney += bettingSystem.mainPot;
        }
        else if (winnerResult == 1)
        {
            winnerCards = deck.AiCard;

            bettingSystem.aiMoney += bettingSystem.mainPot;
        }
        else if (winnerResult == 2)
        {
            isDraw =true;
            Debug.Log("무승부입니다.");
        }

        //이긴 카드 스케일 키움
        if (winnerResult != 2)
        {
            foreach (Card card in winnerCards)
            {
                var sd = card.transform.localScale;
                card.transform.localScale = sd * Vector2.one * 1.5f;
            }
        }
    }

    public void ResetBtn()
    {
        bettingSystem.isFirstBet = true;
        bettingSystem.isSecondBet = false;
        bettingSystem.BettingCount = 0;
        DeckShuffle();
        bettingBtn.SetActive(true);
        if(isDraw == true)
        {
            isDraw= false;
            DrawBetting();

        }
        else if(isDraw == false)
        {
            bettingSystem.ResetBetting();
        }
    }
    

    

}
