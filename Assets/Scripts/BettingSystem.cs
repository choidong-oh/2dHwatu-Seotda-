using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BettingSystem : MonoBehaviour
{
    [Header("�ӽ�")]
    public int playerMoney = 1000000;//�÷��̾� ��
    public int aiMoney = 1000000; //ai��

    public int baseMoney = 10000; //�⺻ ���ñ�
    public int mainPot = 0; //�ѹ��ñ� ���� ����
    public int beforeBettingMoney = 0; //���� ������ �÷��̾��� ���ñ�

    public Button dieButton;
    public Button bbingButton;
    public Button callButton;
    public Button halfButton;
    public Button allInButton;

    public bool isPlayerTurn = false;
    public bool isFirstBet = false;
    public bool isSecondBet = false;

    public TextMeshProUGUI PlayerMoneyText;
    public TextMeshProUGUI AiMoneyText;
    public TextMeshProUGUI MainPotText;
    public TextMeshProUGUI AiBettingNameText;


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
            case "Die":
                //������**
                break;

            case "Bbing":
                beforeBettingMoney = baseMoney;
                break;

            case "Call":
                beforeBettingMoney = beforeBettingMoney;
                break;

            case "Half":
                beforeBettingMoney += (int)(mainPot * 0.5);
                break;

            case "AllIn":
                beforeBettingMoney = money;
                break;

            
            //�ӽ÷� ���Ͽ�?
            case "coffee":
                break;
        }
       

        mainPot += beforeBettingMoney;

        money -= beforeBettingMoney;
        

    }
    

    public void AiBetting(string bettingName)
    {
        if((int)(mainPot * 0.5)>=aiMoney || beforeBettingMoney>=aiMoney)
        {
            Debug.Log("������");
            bettingName = "AllIn";
        }

        Betting(bettingName, ref aiMoney);
        AiBettingNameText.text = bettingName;
    }
    public void PlayerBetting(string bettingName)
    {
        Betting(bettingName, ref playerMoney);
    }

    public void ResetBetting()
    {
        mainPot = 0;
       
        beforeBettingMoney = 0;
    }

    public void AllInTrue()
    {
        dieButton.interactable = false;
        //bbingButton.interactable = false;
        callButton.interactable = false;
        halfButton.interactable = false;
        allInButton.interactable = false;


        // ���� �ƴ� ��� ��ư Ȱ��ȭ
        if (beforeBettingMoney < playerMoney)
        {
            dieButton.interactable = true;
            //bbingButton.interactable = true;
            callButton.interactable = true;
            halfButton.interactable = true;
        }
        //������ �ȵų�?
        if ((int)(mainPot * 0.5) > playerMoney)
        {
            halfButton.interactable = false;
        }
        //���� �ȵų�?
        if (beforeBettingMoney >= playerMoney)
        {
            dieButton.interactable = true;
            allInButton.interactable = true;
        }


        //ù��° �÷��̾ �����̳�?
        if (isFirstBet == true)
        {
            callButton.interactable = false;
        }

    }

    public void UiInteractableFalse()
    {
        dieButton.interactable = false;
        //bbingButton.interactable = false;
        callButton.interactable = false;
        halfButton.interactable = false;
        allInButton.interactable = false;
    } 


    private void Update()
    {
        MainPotText.text = mainPot.ToString();
        PlayerMoneyText.text = playerMoney.ToString();  
        AiMoneyText.text = aiMoney.ToString();

        if (Input.GetKeyDown(KeyCode.W))
        {

            //��������
            AllInTrue();
        }


    }

}
