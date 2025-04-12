using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Deck : MonoBehaviour
{
    public GameObject[] DeckPrefab; //20��
    List<int> usedeck = new List<int>(); //��
    public List<Card> myCard = new List<Card>(); //��ī��
    public Transform[] cardSpawnPositions; //ī�� ���� ��ġ
    int CardPostionArrayNum = 0; //������ġ �迭
    public event Action<int, bool> cardNumUi; //Ȯ�ο� UI
    public event Action<int> testJokboPointUi; //Ȯ�ο� UI
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


    //�� ����
    public void DeckShuffle()
    {
        ResetCard();

        //�� �߰�
        for (int i = 0; i < 20; i++)
        {
            usedeck.Add(i);   
        }

        //�Ǽſ����� �����ο�
        for (int i = usedeck.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
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

    //ī�� ���� �ڽİ�ü�� ����
    void SpawnCard(int cardNum, Transform spawnPos)
    {
        int prefabIndex = cardNum;
        GameObject cardObj = Instantiate(DeckPrefab[prefabIndex], spawnPos.position, spawnPos.rotation, spawnPos);

        cardScript = cardObj.GetComponent<Card>();
        myCard.Add(cardScript);

        //ui�� �̺�Ʈ
        cardNumUi?.Invoke(cardScript.cardNum, cardScript.isGwang);
        Debug.Log("�߰��� ī��: " + cardScript.cardNum + ", ��: " + cardScript.isGwang);
    }

    //��ư �̺�Ʈ �߰��� 0,1 �̷������� ���ڰ������
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
        //�� �ʱ�ȭ
        usedeck.Clear();
        //��ī�� �ʱ�ȭ
        myCard.Clear();
        //ui�� �̺�Ʈ
        cardNumUi?.Invoke(0, false);
        testJokboPointUi?.Invoke(0);

        // ī�� ��ġ�� �ִ� �ڽ� ������Ʈ ���� ����
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

        //ui�� �̺�Ʈ
        testJokboPointUi?.Invoke(JokboPoint);
    }


}
