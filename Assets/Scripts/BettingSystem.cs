using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingSystem : MonoBehaviour
{
    [Header("�ӽ�")]
    public int playerMoney = 1000000;//�÷��̾� ��
    public int aiMoney = 1000000; //ai��

    public int baseMoney = 10000; //�⺻ ���ñ�
    public int currentMoney = 0; //�ѹ��ñ� ���� ����
    public int beforePlayerBettingMoney = 0; //���� ������ �÷��̾��� ���ñ�

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
