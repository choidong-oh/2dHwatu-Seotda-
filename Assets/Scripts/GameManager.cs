using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//4. �����ǰ� string > enum���� 
//5. ai����
//6. ����
//3. json���� playermoney static��������
//7. �뷱�� ����
//8. mainpot�� ����ƽ���� start���� ai���� �ְ����, �������� ����
//9. ���Ӹ޴� ��, �����Ҽ��հ�
//10. ui�ڸ���ġ
public class GameManager : MonoBehaviour
{
    //�⺻���� > ī��й� > ���� > ī��й� > ���� > ���ǰ�
    [SerializeField] BettingSystem bettingSystem;
    [SerializeField] Deck deck;
    [SerializeField] WinnerSystem winnerSystem;
    [SerializeField] string aiBettingName;

    [SerializeField] GameObject bettingBtn;

    bool isDraw;



    //�⺻���ù�ư > ����,ī���ο� > ���� Ȱ��ȭ > ī���ο� > ����Ȱ��ȭ > ���ǰ�
    private void Start()
    {
        //ó�� ui ��Ȱ��ȭ
        bettingSystem.UiInteractableFalse();
        bettingSystem.BettingCount = 0;
    }

    public void BaseBetting()
    {
        bettingSystem.ResetBetting();
        bettingSystem.BaseBetting(ref bettingSystem.playerMoney);
        bettingSystem.BaseBetting(ref bettingSystem.aiMoney);
        bettingBtn.SetActive(false);

        //����,ī���ο�
        DeckShuffle();
        CardDraw();
    }

    void DrawBetting()
    {
        bettingBtn.SetActive(false);

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
    }

    public void Betting(string bettingName)
    {
        bettingSystem.PlayerBetting(bettingName);
        //�÷��̾ �����Ѱ���
        if(bettingName == "Die")
        {
            bettingSystem.isDie = false;
            bettingSystem.aiMoney += bettingSystem.mainPot;
            ResetBtn();
            return;
        }
        bettingSystem.AiBetting(aiBettingName);
        //AI�� �����Ѱ���
        if (aiBettingName == "Die")
        {
            bettingSystem.isDie = false;
            bettingSystem.playerMoney += bettingSystem.mainPot;
            ResetBtn();
            return;
        }
        Debug.Log("dsds");


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
            Debug.Log("���º��Դϴ�.");
        }

        //�̱� ī�� ������ Ű��
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
