using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Deck : MonoBehaviour
{
    public GameObject[] DeckPrefab; //20장
    List<int> usedeck = new List<int>(); //덱
    public List<Card> myCard = new List<Card>(); //내카드
    public List<Card> AiCard = new List<Card>(); //Ai카드

    public Transform[] PlayerCardSpawnPositions; //player카드 놓는 위치
    int PlayerCardPostionArrayNum = 0; //놓는위치 배열
    public Transform[] AiCardSpawnPositions; //ai카드 놓는 위치
    int AiCardPostionArrayNum = 0; //놓는위치 배열

    Card cardScript;

    [Header("테스트용 카드 value")]
    [SerializeField] int cardNum1;
    [SerializeField] bool cardNumIsGwang1;
    [SerializeField] int cardNum2;
    [SerializeField] bool cardNumIsGwang2;

    [SerializeField] int aicardNum1;
    [SerializeField] bool aicardNumIsGwang1;
    [SerializeField] int aicardNum2;
    [SerializeField] bool aicardNumIsGwang2;

    public void DeckStart()
    {
        DeckShuffle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestCard();
        }

    }

    //테스트용 카드 넣어주기
    public void TestCard()
    {
        myCard[0].cardNum = cardNum1;
        myCard[0].isGwang = cardNumIsGwang1;
        myCard[1].cardNum = cardNum2;
        myCard[1].isGwang = cardNumIsGwang2;

        AiCard[0].cardNum = aicardNum1;
        AiCard[0].isGwang = aicardNumIsGwang1;
        AiCard[1].cardNum = aicardNum2;
        AiCard[1].isGwang = aicardNumIsGwang2;

    }



    //덱 셔플
    public void DeckShuffle()
    {
        ResetCard();

        //덱 추가
        for (int i = 0; i < 20; i++)
        {
            usedeck.Add(i);   
        }

        //피셔예이츠 랜덤부여
        for (int i = usedeck.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            var temp = usedeck[i];
            usedeck[i] = usedeck[j];
            usedeck[j] = temp;
        }
    }

    //덱 드로우
    int DrawCard()
    { 
        //마지막패 뽑는거 반환
        var temp = usedeck.Last();
        usedeck.Remove(temp);
        return temp;
    }

    //카드 생성 자식객체에 생성
    void SpawnCard(bool isPlayer, int cardNum, Transform spawnPos)
    {
        int prefabIndex = cardNum;
        GameObject cardObj = Instantiate(DeckPrefab[prefabIndex], spawnPos.position, spawnPos.rotation, spawnPos);

        cardScript = cardObj.GetComponent<Card>();

        //ai인지, player인지
        List<Card> targetCardList = isPlayer ? myCard : AiCard;
        targetCardList.Add(cardScript);

        // 카드 순서 비교해서 자리 바꾸기
        if (targetCardList.Count == 2)
        {
            Card firstCard = targetCardList[0];
            Card secondCard = targetCardList[1];

            if (secondCard.cardNum < firstCard.cardNum)
            {
                // 위치 교환
                Transform firstTransform = firstCard.transform;
                Transform secondTransform = secondCard.transform;

                Vector3 tempPos = firstTransform.position;
                Quaternion tempRot = firstTransform.rotation;

                firstTransform.position = secondTransform.position;
                firstTransform.rotation = secondTransform.rotation;

                secondTransform.position = tempPos;
                secondTransform.rotation = tempRot;

                // 리스트 내 순서도 바꿔주기 
                targetCardList[0] = secondCard;
                targetCardList[1] = firstCard;
            }
        }

        Debug.Log("추가된 카드: " + cardScript.cardNum + ", 광: " + cardScript.isGwang);
    }

    //버튼 이벤트 추가시 0,1 이런식으로 인자값쓰면댐
    public void PlayerSpawnCardBtn()
    {
        if (PlayerCardPostionArrayNum >= 2)
        {
            PlayerCardPostionArrayNum = 0;
        }
        int card = DrawCard();
        SpawnCard(true,card, PlayerCardSpawnPositions[PlayerCardPostionArrayNum]);
        PlayerCardPostionArrayNum++;
        
    }

    public void AiSpawnCardBtn()
    {
        if (AiCardPostionArrayNum >= 2)
        {
            AiCardPostionArrayNum = 0;
        }
        int card = DrawCard();
        SpawnCard(false,card, AiCardSpawnPositions[AiCardPostionArrayNum]);
        AiCardPostionArrayNum++;
    }

    void ResetCard()
    {
        //덱 초기화
        usedeck.Clear();
        //내카드 초기화
        myCard.Clear();
        //Ai카드 초기화
        AiCard.Clear();


        // 카드 위치에 있는 자식 오브젝트 전부 삭제
        foreach (Transform spawn in PlayerCardSpawnPositions)
        {
            for (int i = spawn.childCount - 1; i >= 0; i--)
            {
                Destroy(spawn.GetChild(i).gameObject);
            }
        }
        foreach (Transform spawn in AiCardSpawnPositions)
        {
            for (int i = spawn.childCount - 1; i >= 0; i--)
            {
                Destroy(spawn.GetChild(i).gameObject);
            }
        }


    }



}
