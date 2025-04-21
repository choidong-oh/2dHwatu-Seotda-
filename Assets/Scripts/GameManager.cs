using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� 1 : ī�� ������ �ٸ��� ������ �ȉ� 38���� - 83����

public class GameManager : MonoBehaviour
{
    //�⺻���� > ī��й� > ���� > ī��й� > ���� > ���ǰ�
    [SerializeField] BettingSystem bettingSystem;
    [SerializeField] Deck deck;
    [SerializeField] WinnerSystem winnerSystem;
    [SerializeField] string aiBettingName;

    [SerializeField] GameObject bettingBtn;




    //�⺻���ù�ư > ����,ī���ο� > ���� Ȱ��ȭ > ī���ο� > ����Ȱ��ȭ > ���ǰ�
    private void Start()
    {
        //ó�� ui ��Ȱ��ȭ
        bettingSystem.UiInteractableFalse();
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
        bettingSystem.AiBetting(aiBettingName);

        //ī���ο�
        if (bettingSystem.isSecondBet == false)
        {
            bettingSystem.isSecondBet = true;
            bettingSystem.isFirstBet = false;
            CardDraw();
        }

    }

    public void Winner()
    {
        winnerSystem.Winner();
        bettingSystem.isFirstBet = true;
        bettingSystem.isSecondBet = false;
        DeckShuffle();

    }



}
