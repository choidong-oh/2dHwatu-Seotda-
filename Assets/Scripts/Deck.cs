using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Deck : MonoBehaviour
{
    public GameObject[] DeckPrefab; //20장
    List<int> usedeck = new List<int>(); //덱
    public List<Card> myCard = new List<Card>(); //내카드
    public Transform[] cardSpawnPositions; //카드 놓는 위치
    int CardPostionArrayNum = 0; //놓는위치 배열
    public event Action<int, bool> cardNumUi; //확인용 UI
    public event Action<int> testJokboPointUi; //확인용 UI
    Card cardScript;

    private void Start()
    {
        DeckShuffle();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < usedeck.Count; i++)
            {
                Debug.Log("usedeck : " + usedeck[i]);
            }

        }

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
    void SpawnCard(int cardNum, Transform spawnPos)
    {
        int prefabIndex = cardNum;
        GameObject cardObj = Instantiate(DeckPrefab[prefabIndex], spawnPos.position, spawnPos.rotation, spawnPos);

        cardScript = cardObj.GetComponent<Card>();
        myCard.Add(cardScript);

        //ui용 이벤트
        cardNumUi?.Invoke(cardScript.cardNum, cardScript.isGwang);
        Debug.Log("추가된 카드: " + cardScript.cardNum + ", 광: " + cardScript.isGwang);
    }

    //버튼 이벤트 추가시 0,1 이런식으로 인자값쓰면댐
    public void SpawnCardBtn()
    {
        if (CardPostionArrayNum >= 2)
        {
            CardPostionArrayNum = 0;
        }
        int card = DrawCard();
        SpawnCard(card, cardSpawnPositions[CardPostionArrayNum]);
        CardPostionArrayNum++;
    }

    void ResetCard()
    {
        //덱 초기화
        usedeck.Clear();
        //내카드 초기화
        myCard.Clear();
        //ui용 이벤트
        cardNumUi?.Invoke(0, false);
        testJokboPointUi?.Invoke(0);

        // 카드 위치에 있는 자식 오브젝트 전부 삭제
        foreach (Transform spawn in cardSpawnPositions)
        {
            for (int i = spawn.childCount - 1; i >= 0; i--)
            {
                Destroy(spawn.GetChild(i).gameObject);
            }
        }

    }

    public void testJokboPoint()
    {
        int JokboPoint = myCard[0].cardNum + myCard[1].cardNum;

        //ui용 이벤트
        testJokboPointUi?.Invoke(JokboPoint);
    }


}
