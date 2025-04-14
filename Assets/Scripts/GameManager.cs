using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] UiManager uiManager;

    public void JokboPointBtn()
    {
        List<int> totalPoint = Jokbo.instance.JokboPoint(deck.myCard[0], deck.myCard[1]);
        List<int> AitotalPoint = Jokbo.instance.JokboPoint(deck.AiCard[0], deck.AiCard[1]);
        uiManager.TotalJokboPoint(totalPoint[0]);
        uiManager.AiTotalJokboPoint(AitotalPoint[0]);
        uiManager.WhoWinner(totalPoint[0], AitotalPoint[0]);    

    }

}
