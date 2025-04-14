using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static int playerMoney;

    static public int PlayerMoney
    {
        get { return playerMoney = 100000; }
        set
        {
            if (value <= 0)
            {
                playerMoney = 0;
            }
            else
            {
                playerMoney = value;    
            }
        }
    }



}
