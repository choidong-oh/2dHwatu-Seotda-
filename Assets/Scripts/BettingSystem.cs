using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BettingSystem : MonoBehaviour
{
    [Header("임시")]
    public int playerMoney = 1000000;//플레이어 돈
    public int aiMoney = 1000000; //ai돈

    public int baseMoney = 10000; //기본 배팅금
    public int mainPot = 0; //총배팅금 담을 변수
    public int beforeBettingMoney = 0; //이전 순서의 플레이어의 배팅금

    public void BaseBetting(ref int money)
    {
        beforeBettingMoney = baseMoney;
        money-=baseMoney;
        mainPot += baseMoney;
    }

    
    public void Betting(string bettingName, ref int money)
    {
        if (bettingName == "coffee")
        {
            return;
        }
        switch (bettingName)
        {
            case "Half":
                beforeBettingMoney += (int)(beforeBettingMoney * 0.5);
                break;

            case "Call":
                beforeBettingMoney = beforeBettingMoney;
                break;

            case "DdaDang":
                beforeBettingMoney = beforeBettingMoney*2;
                break;

            case "AllIn":
                beforeBettingMoney = money;
                break;
            case "coffee":
                break;
        }
       

        mainPot += beforeBettingMoney;

        money -= beforeBettingMoney;
        

    }
    private bool BettingTrue(string bettingName, ref int money)
    {
        //내돈이 전배팅금보다 낮으면? 올인
        //하프도 안되게해야대나? 아니면 그냥 하프까지 안되면 바로올인?
        if (money < beforeBettingMoney)
        {
            return true; // 예외 상황
        }

        return false; // 예외가 아닌 경우
    }


    public void AiBetting(string bettingName)
    {
        if (BettingTrue(bettingName, ref aiMoney))
        {
            bettingName = "AllIn";
        }
        Betting(bettingName, ref aiMoney);
    }
    public void PlayerBetting(string bettingName)
    {
        if (BettingTrue(bettingName, ref playerMoney))
        {
            bettingName = "AllIn";
        }
        Betting(bettingName, ref playerMoney);
    }

    public void ResetBetting()
    {
        mainPot = 0;
       
        beforeBettingMoney = 0;
    }

    

}
