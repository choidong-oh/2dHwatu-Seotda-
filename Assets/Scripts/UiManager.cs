using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI cardNum1;
    public TextMeshProUGUI cardNum2;
    public TextMeshProUGUI jokboPoint;

    private void Update()
    {




    }
    private void OnEnable()
    {
        
    }

    void FirstUpdateCard(int cardNum, bool isGwang)
    {
        cardNum1.text = cardNum.ToString();
    }

    void SecondUpdateCard(int cardNum, bool isGwang)
    {
        cardNum2.text = cardNum.ToString();
    }

    void TotalJokboPoint()
    {

    }
}
