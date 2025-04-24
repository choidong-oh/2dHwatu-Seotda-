using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

//9. ��ο�� ���� �ʱ�ȭ
//10. ui�ڸ���ġ, ui���̾ƿ��ؼ��ϸ�ɵ�(�ػ󵵴���)
//7. �뷱�� ����
//15.������ �̻ڰ� �����, õõ��
public class GameManager : MonoBehaviour
{
    //�⺻���� > ī��й� > ���� > ī��й� > ���� > ���ǰ�
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




    //�⺻���ù�ư > ����,ī���ο� > ���� Ȱ��ȭ > ī���ο� > ����Ȱ��ȭ > ���ǰ�
    private void Start()
    {
        jsonManager.JsonStart();
        deck.DeckStart();
        jokboUi.JokboUiStart();

        //�� �ҷ�����
        jsonManager.MoneyLoad();

        //ó�� ui ��Ȱ��ȭ
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

        //json ������
        jsonManager.MoneySave();

        //����,ī���ο�
        DeckShuffle();
        CardDraw();
    }

    void DrawBetting()
    {
        bettingBtn.SetActive(false);
        jokboUi.ResetJokboUi();
        //����,ī���ο�
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

        //����Ȱ��ȭ
        bettingSystem.AllInTrue();

        //2����ī���ο�
        CardDrawCount++;
        if (CardDrawCount >= 2)
        {
            var Whatjokbo = jokbo.JokboPoint(deck.myCard[0], deck.myCard[1])[2];
            //�����̹���
            jokboUi.HighlightRank(Whatjokbo, Color.yellow);
        }

    }

    public void Betting(string bettingName)
    {
        bettingSystem.PlayerBetting(bettingName);
        //�÷��̾ �����Ѱ���
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
        //AI�� �����Ѱ���
        if (aiBetting.aiBettingName == "Die")
        {
            bettingSystem.isDie = false;
            Player.playerMoney += BettingSystem.mainPot;
            BettingSystem.mainPot = 0;
            ResetBtn();
            return;
        }
        Debug.Log("dsds");

        //json ������
        jsonManager.MoneySave();

        //ī���ο�
        if (bettingSystem.isSecondBet == false)
        {
            bettingSystem.isSecondBet = true;
            bettingSystem.isFirstBet = false;
            CardDraw();
        }

        bettingSystem.BettingCount++;

        //�ι� �����ϸ� ���ǰ��
        if(bettingSystem.BettingCount >= 2)
        {
            bettingSystem.UiInteractableFalse();

            //���ǰ� �̱��� ������ Ű���
            Winner();

            //��ư ��Ȱ��ȭ
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
            Debug.Log("���º��Դϴ�.");
        }

        //json ������
        jsonManager.MoneySave();

        //�̱� ī�� ������ Ű��
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
            //���ӿ���
            GameEnding(Player.playerMoney, true);
            GameEnding(Ai.AiMoney, false);
        }
    }

    void GameEnding(int Money, bool isPlayer)
    {
        if (isPlayer == true)
        {
            //���� ����
            if (Money <= bettingSystem.baseMoney)
            {
                BettingSystem.mainPot = 0;
                jsonManager.MoneySave();
                SceneChangeManager.instance.SceneChange("0");
            }
        }
        else if(isPlayer == false) 
        {
            //���� ����
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

        //json ������
        jsonManager.MoneySave();
    }
    

    

}
