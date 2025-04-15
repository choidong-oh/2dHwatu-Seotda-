using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingSystem : MonoBehaviour
{
    [Header("임시")]
    public int playerMoney = 1000000;//플레이어 돈
    public int aiMoney = 1000000; //ai돈

    public int baseMoney = 10000; //기본 배팅금
    public int currentMoney = 0; //총배팅금 담을 변수
    public int beforePlayerBettingMoney = 0; //이전 순서의 플레이어의 배팅금

    public void Betting(string bettingName, ref int money)
    {
        switch (bettingName)
        { 
            case "Base":
                beforePlayerBettingMoney = baseMoney;
                money -= beforePlayerBettingMoney;
                currentMoney += beforePlayerBettingMoney;
                break;

            case "Half" :
                beforePlayerBettingMoney = beforePlayerBettingMoney* 2;
                money -= beforePlayerBettingMoney;
                currentMoney += beforePlayerBettingMoney;
                break;
        }

    }
    public void AiBetting(string bettingName)
    {
        Betting(bettingName, ref aiMoney);
    }
    public void PlayerBetting(string bettingName)
    {
        Betting(bettingName, ref playerMoney);
    }

    public void ResetBetting()
    {
        currentMoney = 0;
    }

    

}
