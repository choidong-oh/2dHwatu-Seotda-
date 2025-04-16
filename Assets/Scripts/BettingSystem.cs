using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BettingSystem : MonoBehaviour
{
    [Header("�ӽ�")]
    public int playerMoney = 1000000;//�÷��̾� ��
    public int aiMoney = 1000000; //ai��

    public int baseMoney = 10000; //�⺻ ���ñ�
    public int mainPot = 0; //�ѹ��ñ� ���� ����
    public int beforeBettingMoney = 0; //���� ������ �÷��̾��� ���ñ�
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
        // �÷��̾ �� �� ���� ���� (Half, Call)
        if (bettingName == "Half" || bettingName == "Call")
        {
            // Half�� Call�� ���� ������ 0�̰ų�, �÷��̾ ���� ���� ������ ��� ����� �� ����
            if (beforeBettingMoney == 0 || money < beforeBettingMoney)
            {
                return true; // ���� ��Ȳ
            }
        }
        return false; // ���ܰ� �ƴ� ���
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
