using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���⿡ ���� �ְ� ���� ��ȯ������ ��
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
        JokboPoint.Add(mycard1.cardNum + mycard2.cardNum); //�ӽ� ���ϱ�
        JokboPoint.Add(0); //Ư����Ģ �ƴѰ��
        return JokboPoint;
    }

}
