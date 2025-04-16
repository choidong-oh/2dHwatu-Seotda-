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


    public void BaseBetting()
    {
        bettingSystem.ResetBetting();
        bettingSystem.BaseBetting(ref bettingSystem.playerMoney);
        bettingSystem.BaseBetting(ref bettingSystem.aiMoney);
    }

    public void DeckShuffle()
    {
        deck.DeckShuffle();
    }

    public void CardDraw()
    {
        deck.PlayerSpawnCardBtn();
        deck.AiSpawnCardBtn();
    }

    public void Betting(string bettingName)
    {
        bettingSystem.PlayerBetting(bettingName);
        bettingSystem.AiBetting(aiBettingName);
    }

    public void Winner()
    {
        winnerSystem.Winner();

    }



}
