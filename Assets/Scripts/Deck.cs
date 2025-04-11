using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Card card;
    public GameObject[] DeckPrefab; //20jang  
    List<Card> usedeck = new List<Card>();
    public Transform[] cardSpawnPositions;

    private void Start()
    {
        DeckShuffle();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("0��° ī�� ���� : " + usedeck[0].cardNum);
            Debug.Log("0��° ī�� ������ : " + usedeck[0].isGwang);
            Debug.Log("������° ī�� ���� : " + DrawCard().cardNum);
            Debug.Log("������° ī�� ���� : " + DrawCard().isGwang);
        }
       


    }


    //�� ����
    void DeckShuffle()
    {
        //�� �ʱ�ȭ
        usedeck.Clear();

        //�� �߰�
        for (int i = 0; i < 2; i++)
        {
            for ( int j = 0; j <= 10; j++)
            {
                card = new Card();
                card.cardNum = j;
                usedeck.Add(card);
            }
        }

        usedeck[0].isGwang = true;
        usedeck[4].isGwang = true;
        usedeck[15].isGwang = true;

        //�Ǽſ����� �����ο�
        for (int i = usedeck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card temp = usedeck[i];
            usedeck[i] = usedeck[j];
            usedeck[j] = temp;
        }
    }

    //�� ��ο�
    Card DrawCard()
    { 
        //�������� �̴°� ��ȯ
        var temp = usedeck.Last();
        usedeck.Remove(temp);
        return temp;
    }

    //ī�� ����
    void SpawnCard(Card card, Transform spawnPos)
    {
        int prefabIndex = card.cardNum; 
        Instantiate(DeckPrefab[prefabIndex], spawnPos.position, Quaternion.identity);
    }

    public void SpawnCardBtn()
    {
        Card card = DrawCard();
        SpawnCard(card, cardSpawnPositions[0]);
    }


}
