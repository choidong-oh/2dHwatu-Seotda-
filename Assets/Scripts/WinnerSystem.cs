using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public enum WhoWinner
    {
        PlayerWin = 0, AiWin, Draw, x
    }

public class WinnerSystem : MonoBehaviour
{

    [SerializeField] Deck deck;

    public int Winner()
    {
        List<int> PlayerPoint = Jokbo.instance.JokboPoint(deck.myCard[0], deck.myCard[1]);
        List<int> AiPoint = Jokbo.instance.JokboPoint(deck.AiCard[0], deck.AiCard[1]);

        int pScore = PlayerPoint[0];
        int pSpecial = PlayerPoint[1];

        int aiScore = AiPoint[0];
        int aiSpecial = AiPoint[1];

        string result = "Draw"; // �⺻��

        //1. 0 Ư����Ģ����
        if (PlayerPoint[1] == 0 && AiPoint[1] == 0)
        {
            result = PlayerPoint[0] > AiPoint[0] ? "PlayerWin" : (PlayerPoint[0] < AiPoint[0] ? "AiWin" : "Draw");
            Debug.Log(result);
        }
        //2. 1 ������ 
        else if (pSpecial == 1)
        {
            //������ ����
            if (aiScore % 10 == 0 && aiScore != 200 && aiScore != 0)
            {
                result = "��PlayerWin";
            }
            //49��������
            else if (aiSpecial == 3 && aiScore < 200)
            {
                result = "Draw";
            }
            else
            {
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
            }
        }
        else if (aiSpecial == 1)
        {
            // ������ ����
            if (pScore % 10 == 0 && pScore != 200 && pScore != 0)
            {
                result = "��AiWin";
            }
            //49��������
            else if (pSpecial == 3 && pScore < 200)
            {
                result = "Draw";
            }
            else
            {
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
            }
        }
        //3. 2 ������
        else if (pSpecial == 2)
        {
            if (aiScore == 951)
                result = "PlayerWin";
            else if (aiSpecial == 3 && aiScore < 200)
                result = "Draw";
            else
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
        }
        else if (aiSpecial == 2)
        {
            if (pScore == 951)
                result = "AiWin";
            else if (pSpecial == 3 && pScore < 200)
                result = "Draw";
            else
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
        }
        //4. 3 �籸����
        else if (pSpecial == 3 || aiSpecial == 3)
        {
            if (pScore < 200 && aiScore < 200)
                result = "Draw";
            else
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
        }

        Debug.Log(result);

        int WhoWin;
        //enum���� �ٲܰ���
        if(result == "PlayerWin")
        {
            WhoWin = 0;
        }
        else if(result == "AiWin")
        {
            WhoWin = 1;
        }
        else
        {
            WhoWin = 2;
        }
        return WhoWin;
    }



}
