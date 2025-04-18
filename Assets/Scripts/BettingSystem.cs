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
        //������ �����ñݺ��� ������? ����
        //������ �ȵǰ��ؾߴ볪? �ƴϸ� �׳� �������� �ȵǸ� �ٷο���?
        if (money < beforeBettingMoney)
        {
            return true; // ���� ��Ȳ
        }

        return false; // ���ܰ� �ƴ� ���
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
