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
    public int PlayerSidePot = 0; // 
    public int AiSidePot = 0; // 

    public void BaseBetting(ref int money)
    {
        beforeBettingMoney = baseMoney;
        money-=baseMoney;
        mainPot += baseMoney;
    }

    
    public void Betting(string bettingName, ref int money, bool isPlayer)
    {
        if (bettingName == "coffee")
        {
            return;
        }
        switch (bettingName)
        {
            case "Base":
                beforeBettingMoney = baseMoney;
                break;

            case "Half":
                beforeBettingMoney = beforeBettingMoney * 2;
                break;

            case "Call":
                beforeBettingMoney = beforeBettingMoney;
                break;

            case "AllIn":
                beforeBettingMoney = money;
                break;
            case "coffee":
                break;
        }
       

        mainPot += beforeBettingMoney;
        if(money< beforeBettingMoney)
        {
            if (isPlayer)
            {
                AiSidePot += beforeBettingMoney - money;
            }
            else
            {
                PlayerSidePot += beforeBettingMoney - money;
            }
            money = 0;
        }
        else
        {
            money -= beforeBettingMoney;
        }

    }
    private bool BettingTrue(string bettingName, ref int money)
    {
        // 플레이어가 할 수 없는 배팅 (Half, Call)
        if (bettingName == "Half" || bettingName == "Call")
        {
            // Half나 Call은 이전 배팅이 0이거나, 플레이어가 가진 돈이 부족한 경우 사용할 수 없음
            if (beforeBettingMoney == 0 || money < beforeBettingMoney)
            {
                return true; // 예외 상황
            }
        }
        return false; // 예외가 아닌 경우
    }


    public void AiBetting(string bettingName)
    {
        //if (BettingTrue(bettingName,ref aiMoney))
        //{
        //    bettingName = "AllIn";
        //}
        Betting(bettingName, ref aiMoney,false);
    }
    public void PlayerBetting(string bettingName)
    {
        //if (BettingTrue(bettingName, ref playerMoney))
        //{
        //    bettingName = "AllIn";
        //}
        Betting(bettingName, ref playerMoney,true);
    }

    public void ResetBetting()
    {
        mainPot = 0;
        AiSidePot = 0;
        PlayerSidePot = 0;
        beforeBettingMoney = 0;
    }

    

}
