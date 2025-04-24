using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

//9. 드로우시 족보 초기화
//10. ui자리배치, ui레이아웃해서하면될듯(해상도대응)
//7. 밸런스 조정
//15.마지막 이쁘게 만들기, 천천히
public class GameManager : MonoBehaviour
{
    //기본배팅 > 카드분배 > 배팅 > 카드분배 > 배팅 > 승판결
    [SerializeField] BettingSystem bettingSystem;
    [SerializeField] Deck deck;
    [SerializeField] WinnerSystem winnerSystem;
    [SerializeField] JokboUi jokboUi;
    [SerializeField] Jokbo jokbo;
    [SerializeField] AiBetting aiBetting;
    [SerializeField] JsonManager jsonManager;

    [SerializeField] GameObject bettingBtn;
    [SerializeField] GameObject IsDrawObj;

    [SerializeField] Button SceneChangeBtn;
    bool isDraw;
    int CardDrawCount;




    //기본배팅버튼 > 셔플,카드드로우 > 배팅 활성화 > 카드드로우 > 배팅활성화 > 승판결
    private void Start()
    {
        jsonManager.JsonStart();
        deck.DeckStart();
        jokboUi.JokboUiStart();

        //돈 불러오기
        jsonManager.MoneyLoad();

        //처음 ui 비활성화
        bettingSystem.UiInteractableFalse();
        bettingSystem.BettingCount = 0;
        CardDrawCount = 0;

        //test
        SceneChangeBtn.onClick.AddListener(()=> SceneChangeManager.instance.SceneChange("0"));
    }

    public void BaseBetting()
    {
        bettingSystem.UiInteractableFalse();
        bettingSystem.ResetBetting();
        bettingSystem.BaseBetting(ref Player.playerMoney);
        bettingSystem.BaseBetting(ref Ai.AiMoney);
        bettingBtn.SetActive(false);

        //json 돈저장
        jsonManager.MoneySave();

        //셔플,카드드로우
        DeckShuffle();
        CardDraw();
    }

    void DrawBetting()
    {
        bettingBtn.SetActive(false);
        jokboUi.ResetJokboUi();
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

        //2번쨰카드드로우
        CardDrawCount++;
        if (CardDrawCount >= 2)
        {
            var Whatjokbo = jokbo.JokboPoint(deck.myCard[0], deck.myCard[1])[2];
            //족보이미지
            jokboUi.HighlightRank(Whatjokbo, Color.yellow);
        }

    }

    public void Betting(string bettingName)
    {
        bettingSystem.PlayerBetting(bettingName);
        //플레이어가 다이한거임
        if(bettingName == "Die")
        {
            bettingSystem.isDie = false;
            Ai.AiMoney += BettingSystem.mainPot;
            BettingSystem.mainPot = 0;
            ResetBtn();
            return;
        }
        aiBetting.RandomAiBetting2();
        bettingSystem.AiBetting(aiBetting.aiBettingName);
        //AI가 다이한거임
        if (aiBetting.aiBettingName == "Die")
        {
            bettingSystem.isDie = false;
            Player.playerMoney += BettingSystem.mainPot;
            BettingSystem.mainPot = 0;
            ResetBtn();
            return;
        }
        Debug.Log("dsds");

        //json 돈저장
        jsonManager.MoneySave();

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

            Player.playerMoney += BettingSystem.mainPot;
        }
        else if (winnerResult == 1)
        {
            winnerCards = deck.AiCard;

            Ai.AiMoney += BettingSystem.mainPot;
        }
        else if (winnerResult == 2)
        {
            isDraw =true;
            IsDrawObj.SetActive(true);
            Debug.Log("무승부입니다.");
        }

        //json 돈저장
        jsonManager.MoneySave();

        //이긴 카드 스케일 키움
        if (winnerResult != 2)
        {
            foreach (Card card in winnerCards)
            {
                var sd = card.transform.localScale;
                card.transform.localScale = sd * Vector2.one * 1.5f;
            }
        }

        if (isDraw == false)
        {
            //게임엔딩
            GameEnding(Player.playerMoney, true);
            GameEnding(Ai.AiMoney, false);
        }
    }

    void GameEnding(int Money, bool isPlayer)
    {
        if (isPlayer == true)
        {
            //게임 엔딩
            if (Money <= bettingSystem.baseMoney)
            {
                BettingSystem.mainPot = 0;
                jsonManager.MoneySave();
                SceneChangeManager.instance.SceneChange("0");
            }
        }
        else if(isPlayer == false) 
        {
            //게임 엔딩
            if (Money <= bettingSystem.baseMoney)
            {
                BettingSystem.mainPot = 0;
                jsonManager.MoneySave();
                SceneChangeManager.instance.SceneChange("2");
            }
        }
       
    }

    public void ResetBtn()
    {
        bettingSystem.UiInteractableFalse();
        jokboUi.ResetJokboUi();
        bettingSystem.isFirstBet = true;
        bettingSystem.isSecondBet = false;
        bettingSystem.BettingCount = 0;

        DeckShuffle();
        if(Player.playerMoney <=bettingSystem.baseMoney)
        {
            GameEnding(Player.playerMoney, true);
        }
        else if(Ai.AiMoney <= bettingSystem.baseMoney)
        {
            GameEnding(Ai.AiMoney, false);
        }
            bettingBtn.SetActive(true);
        CardDrawCount = 0;
        IsDrawObj.SetActive(false);
        if (isDraw == true)
        {
            isDraw= false;
            DrawBetting();

        }
        else if(isDraw == false)
        {
            bettingSystem.ResetBetting();
        }

        //json 돈저장
        jsonManager.MoneySave();
    }
    

    

}
