using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Deck : MonoBehaviour
{
    public GameObject[] DeckPrefab; //20��
    List<int> usedeck = new List<int>(); //��
    public List<Card> myCard = new List<Card>(); //��ī��
    public List<Card> AiCard = new List<Card>(); //Aiī��

    public Transform[] PlayerCardSpawnPositions; //playerī�� ���� ��ġ
    int PlayerCardPostionArrayNum = 0; //������ġ �迭
    public Transform[] AiCardSpawnPositions; //aiī�� ���� ��ġ
    int AiCardPostionArrayNum = 0; //������ġ �迭

    Card cardScript;

    [Header("�׽�Ʈ�� ī�� value")]
    [SerializeField] int cardNum1;
    [SerializeField] bool cardNumIsGwang1;
    [SerializeField] int cardNum2;
    [SerializeField] bool cardNumIsGwang2;

    [SerializeField] int aicardNum1;
    [SerializeField] bool aicardNumIsGwang1;
    [SerializeField] int aicardNum2;
    [SerializeField] bool aicardNumIsGwang2;



    private void Start()
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

    //�׽�Ʈ�� ī�� �־��ֱ�
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
    void SpawnCard(bool isPlayer, int cardNum, Transform spawnPos)
    {
        int prefabIndex = cardNum;
        GameObject cardObj = Instantiate(DeckPrefab[prefabIndex], spawnPos.position, spawnPos.rotation, spawnPos);

        cardScript = cardObj.GetComponent<Card>();
        
        //ai����, player����
        (isPlayer ? myCard : AiCard).Add(cardScript);

        Debug.Log("�߰��� ī��: " + cardScript.cardNum + ", ��: " + cardScript.isGwang);
    }

    //��ư �̺�Ʈ �߰��� 0,1 �̷������� ���ڰ������
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
        //�� �ʱ�ȭ
        usedeck.Clear();
        //��ī�� �ʱ�ȭ
        myCard.Clear();
        //Aiī�� �ʱ�ȭ
        AiCard.Clear();


        // ī�� ��ġ�� �ִ� �ڽ� ������Ʈ ���� ����
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
