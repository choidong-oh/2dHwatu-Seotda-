using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//여기에 족보 넣고 점수 반환식으로 함
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
        JokboPoint.Add(mycard1.cardNum + mycard2.cardNum); //임시 더하기
        JokboPoint.Add(0); //특수규칙 아닌경우
        return JokboPoint;
    }

}
