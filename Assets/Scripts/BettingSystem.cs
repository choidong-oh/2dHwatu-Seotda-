using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BettingSystem : MonoBehaviour
{
    [Header("임시")]
    public int playerMoney = 1000000;//플레이어 돈
    public int aiMoney = 1000000; //ai돈

    public int baseMoney = 10000; //기본 배팅금
    public int mainPot = 0; //총배팅금 담을 변수
    public int beforeBettingMoney = 0; //이전 순서의 플레이어의 배팅금

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
                //다이임**
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

            
            //임시로 리턴용?
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
            Debug.Log("ㅇㄴㅇ");
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


        // 올인 아닌 경우 버튼 활성화
        if (beforeBettingMoney < playerMoney)
        {
            dieButton.interactable = true;
            //bbingButton.interactable = true;
            callButton.interactable = true;
            halfButton.interactable = true;
        }
        //하프가 안돼냐?
        if ((int)(mainPot * 0.5) > playerMoney)
        {
            halfButton.interactable = false;
        }
        //콜이 안돼냐?
        if (beforeBettingMoney >= playerMoney)
        {
            dieButton.interactable = true;
            allInButton.interactable = true;
        }


        //첫번째 플레이어가 배팅이냐?
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

            //올인인지
            AllInTrue();
        }


    }

}
