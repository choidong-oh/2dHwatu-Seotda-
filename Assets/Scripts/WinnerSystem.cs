using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerSystem : MonoBehaviour
{
    public GameObject[] cardPostion;

    private void Start()
    {
        for (int i = 0; i < cardPostion.Length; i++)
        {
            cardPostion[i].transform.GetChild(0).GetComponent<Card>();
        }
    }

}
