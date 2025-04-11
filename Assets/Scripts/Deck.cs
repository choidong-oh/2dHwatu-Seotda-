using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Card card;
    public GameObject[] DeckPrefab; //20jang  
    List<int> usedeck = new List<int>();
    public Transform[] cardSpawnPositions;

    private void Start()
    {
        DeckShuffle();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.Log("0번째 카드 숫자 : " + usedeck[0].cardNum);
        //    Debug.Log("0번째 카드 광인지 : " + usedeck[0].isGwang);
        //    Debug.Log("마지막째 카드 숫자 : " + DrawCard().cardNum);
        //    Debug.Log("마지막째 카드 숫자 : " + DrawCard().isGwang);
        //}
       


    }


    //덱 셔플
    void DeckShuffle()
    {
        //덱 초기화
        usedeck.Clear();

        //덱 추가
        for (int i = 0; i < 20; i++)
        {
            usedeck.Add(i);   
        }

        //피셔예이츠 랜덤부여
        for (int i = usedeck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
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

    //카드 생성
    void SpawnCard(int cardNum, Transform spawnPos)
    {
        int prefabIndex = cardNum; 
        Instantiate(DeckPrefab[prefabIndex], spawnPos.position, spawnPos.rotation);
    }

    public void SpawnCardBtn()
    {
        int card = DrawCard();
        SpawnCard(card, cardSpawnPositions[0]);
    }


}
