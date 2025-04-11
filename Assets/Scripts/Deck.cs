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
        //    Debug.Log("0��° ī�� ���� : " + usedeck[0].cardNum);
        //    Debug.Log("0��° ī�� ������ : " + usedeck[0].isGwang);
        //    Debug.Log("������° ī�� ���� : " + DrawCard().cardNum);
        //    Debug.Log("������° ī�� ���� : " + DrawCard().isGwang);
        //}
       


    }


    //�� ����
    void DeckShuffle()
    {
        //�� �ʱ�ȭ
        usedeck.Clear();

        //�� �߰�
        for (int i = 0; i < 20; i++)
        {
            usedeck.Add(i);   
        }

        //�Ǽſ����� �����ο�
        for (int i = usedeck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = usedeck[i];
            usedeck[i] = usedeck[j];
            usedeck[j] = temp;
        }
    }

    //�� ��ο�
    int DrawCard()
    { 
        //�������� �̴°� ��ȯ
        var temp = usedeck.Last();
        usedeck.Remove(temp);
        return temp;
    }

    //ī�� ����
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
