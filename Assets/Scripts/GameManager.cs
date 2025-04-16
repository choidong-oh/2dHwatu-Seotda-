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
