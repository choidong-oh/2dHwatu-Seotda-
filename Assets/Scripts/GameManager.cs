using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//문제 1 : 카드 순서가 다르면 족보가 안됌 38광떙 - 83광땡

public class GameManager : MonoBehaviour
{
    //기본배팅 > 카드분배 > 배팅 > 카드분배 > 배팅 > 승판결
    [SerializeField] BettingSystem bettingSystem;
    [SerializeField] Deck deck;
    [SerializeField] WinnerSystem winnerSystem;
    [SerializeField] string aiBettingName;

    [SerializeField] GameObject bettingBtn;




    //기본배팅버튼 > 셔플,카드드로우 > 배팅 활성화 > 카드드로우 > 배팅활성화 > 승판결
    private void Start()
    {
        //처음 ui 비활성화
        bettingSystem.UiInteractableFalse();
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
        bettingSystem.AiBetting(aiBettingName);

        //카드드로우
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
