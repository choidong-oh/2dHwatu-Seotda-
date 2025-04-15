using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinnerSystem : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] UiManager uiManager;
    string WhoWinner;

    public void JokboPointBtn()
    {
        List<int> totalPoint = Jokbo.instance.JokboPoint(deck.myCard[0], deck.myCard[1]);
        List<int> AitotalPoint = Jokbo.instance.JokboPoint(deck.AiCard[0], deck.AiCard[1]);
        uiManager.TotalJokboPoint(totalPoint[0]);
        uiManager.AiTotalJokboPoint(AitotalPoint[0]);
        uiManager.WhoWinner(totalPoint[0], AitotalPoint[0]);    

    }

    public void Winner()
    {
        List<int> PlayerPoint = Jokbo.instance.JokboPoint(deck.myCard[0], deck.myCard[1]);
        List<int> AiPoint = Jokbo.instance.JokboPoint(deck.AiCard[0], deck.AiCard[1]);

        int pScore = PlayerPoint[0];
        int pSpecial = PlayerPoint[1];

        int aiScore = AiPoint[0];
        int aiSpecial = AiPoint[1];

        string result = "Draw"; // 기본값

        //1. 0 특수규칙없음
        if (PlayerPoint[1] == 0 && AiPoint[1] == 0)
        {
            result = PlayerPoint[0] > AiPoint[0] ? "PlayerWin" : (PlayerPoint[0] < AiPoint[0] ? "AiWin" : "Draw");
            Debug.Log(result);
        }
        //2. 1 땡잡이 
        else if (pSpecial == 1)
        {
            //땡잡이 성공
            if (aiScore % 10 == 0 && aiScore != 200 && aiScore != 0)
            {
                result = "땡PlayerWin";
            }
            //49파토인지
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
            // 땡잡이 성공
            if (pScore % 10 == 0 && pScore != 200 && pScore != 0)
            {
                result = "땡AiWin";
            }
            //49파토인지
            else if (pSpecial == 3 && pScore < 200)
            {
                result = "Draw";
            }
            else
            {
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
            }
        }
        //3. 2 암행어사
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
        //4. 3 사구파토
        else if (pSpecial == 3 || aiSpecial == 3)
        {
            if (pScore < 200 && aiScore < 200)
                result = "Draw";
            else
                result = pScore > aiScore ? "PlayerWin" : (pScore < aiScore ? "AiWin" : "Draw");
        }

        Debug.Log(result);

    }



}
