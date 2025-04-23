using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¿©±â¿¡ Á·º¸ ³Ö°í Á¡¼ö ¹ÝÈ¯½ÄÀ¸·Î ÇÔ
public class Jokbo : MonoBehaviour
{
    public static Jokbo instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public List<int> JokboPoint(Card mycard1, Card mycard2)
    {
        List<int> JokboPoint = new List<int>();
        ////Å×½ºÆ® ´õÇÏ±â
        //JokboPoint.Add(mycard1.cardNum + mycard2.cardNum);
        //JokboPoint.Add(0); //Æ¯¼ö±ÔÄ¢ ¾Æ´Ñ°æ¿ì

        //Á·º¸ ½ÃÀÛ
        if (mycard1.isGwang == true && mycard1.cardNum == 3 && mycard2.isGwang == true && mycard2.cardNum == 8)
        {
            JokboPoint.Add(1001);
            JokboPoint.Add(0);
            JokboPoint.Add(1);
            Debug.Log("38±¤¶¯");
            return JokboPoint;
        }
        else if (mycard1.isGwang == true && mycard1.cardNum == 1 && mycard2.isGwang == true && mycard2.cardNum == 3)
        {
            JokboPoint.Add(951);
            JokboPoint.Add(0);
            JokboPoint.Add(2);
            Debug.Log("13±¤¶¯");
            return JokboPoint;
        }
        else if (mycard1.isGwang == true && mycard1.cardNum == 1 && mycard2.isGwang == true && mycard2.cardNum == 8)
        {
            JokboPoint.Add(951);
            JokboPoint.Add(0);
            JokboPoint.Add(2);
            Debug.Log("18±¤¶¯");
            return JokboPoint;
        }

        // Check for other "¶¯" combinations (pairs of the same number)
        if (mycard1.cardNum == 10 && mycard2.cardNum == 10)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("Àå¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 9 && mycard2.cardNum == 9)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("9¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 8 && mycard2.cardNum == 8)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("8¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 7 && mycard2.cardNum == 7)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("7¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 6 && mycard2.cardNum == 6)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("6¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 5 && mycard2.cardNum == 5)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("5¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 4 && mycard2.cardNum == 4)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("4¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 3 && mycard2.cardNum == 3)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("3¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 2 && mycard2.cardNum == 2)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("2¶¯");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 1 && mycard2.cardNum == 1)
        {
            JokboPoint.Add(100 + mycard1.cardNum * 10);
            JokboPoint.Add(0);
            JokboPoint.Add(3);
            Debug.Log("1¶¯");
            return JokboPoint;
        }

        // Check for specific special rules
        else if (mycard1.cardNum == 1 && mycard2.cardNum == 2)
        {
            JokboPoint.Add(56);
            JokboPoint.Add(0);
            JokboPoint.Add(4);
            Debug.Log("¾Ë¸®");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 1 && mycard2.cardNum == 4)
        {
            JokboPoint.Add(55);
            JokboPoint.Add(0);
            JokboPoint.Add(5);
            Debug.Log("µ¶»ç");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 1 && mycard2.cardNum == 9)
        {
            JokboPoint.Add(54);
            JokboPoint.Add(0);
            JokboPoint.Add(6);
            Debug.Log("±¸»æ");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 1 && mycard2.cardNum == 10)
        {
            JokboPoint.Add(53);
            JokboPoint.Add(0);
            JokboPoint.Add(7);
            Debug.Log("Àå»æ");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 4 && mycard2.cardNum == 10)
        {
            JokboPoint.Add(52);
            JokboPoint.Add(0); 
            JokboPoint.Add(8);
            Debug.Log("Àå»ç");
            return JokboPoint;
        }
        else if (mycard1.cardNum == 4 && mycard2.cardNum == 6)
        {
            JokboPoint.Add(51);
            JokboPoint.Add(0);
            JokboPoint.Add(9);
            Debug.Log("¼¼·ú");
            return JokboPoint;
        }

        // Special rule for "¶¯ÀâÀÌ"
        else if (mycard1.cardNum == 3 && mycard2.cardNum == 7 && (mycard1.isGwang == true || mycard2.isGwang == true))
        {
            JokboPoint.Add(0); // No points
            JokboPoint.Add(1); // Special case flag
            JokboPoint.Add(10);
            Debug.Log("¶¯ÀâÀÌ");
            return JokboPoint;
        }

        // Special rule for "¾ÏÇà¾î»ç"
        else if (mycard1.cardNum == 4 && mycard2.cardNum == 7)
        {
            JokboPoint.Add(1); // 1 point
            JokboPoint.Add(2); // Special case flag
            JokboPoint.Add(11);
            Debug.Log("¾ÏÇà¾î»ç");
            return JokboPoint;
        }

        // Special rule for "¸ÛÅÖ±¸¸® ±¸»ç"
        else if (mycard1.cardNum == 4 && mycard2.cardNum == 9)
        {
            JokboPoint.Add(3); // Special points
            JokboPoint.Add(3); // Special case flag
            JokboPoint.Add(12);
            Debug.Log("¸ÛÅÖ±¸¸® ±¸»ç");
            return JokboPoint;
        }
        else
        {
            var jokboPoint = mycard1.cardNum + mycard2.cardNum;

            if (jokboPoint > 10)
            {
                jokboPoint -= 10;
            }

            JokboPoint.Add(jokboPoint);
            JokboPoint.Add(0); 
            JokboPoint.Add(13);
            Debug.Log(jokboPoint + "²ý");
            return JokboPoint;
        }

    }
    

}
