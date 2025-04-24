using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public enum JokboType
{
    SamPalGwangDdaeng = 1,  // ���ȱ���
    GwangDdaeng,            // ����
    Ddaeng,                 // ��
    Ali,                    // �˸�
    Doksa,                  // ����
    GuPping,                // ����
    JangPping,              // ���
    JangSa,                 // ���
    SeRuk,                  // ����
    DdaengJabi,             // ������
    GuSa,                   // ����
    AmhaengEosa,            // ������
    Ggut                    // ��
}

public class JokboUi : MonoBehaviour
{
    [SerializeField] GameObject JokboPanel;

    public GameObject[] JokboTextPrefab;

    public void JokboUiStart()
    {
        foreach (GameObject prefab in JokboTextPrefab)
        {
            Instantiate(prefab, JokboPanel.transform);
        }

    }
   
    public void HighlightRank(int myJokbo, Color highlightColor)
    {
        var ddd = JokboPanel.transform.GetChild(myJokbo - 1).GetComponent<TextMeshProUGUI>();
        ddd.color = highlightColor;
    }

    
    public void ResetJokboUi()
    {
        for (int i = 0; i < JokboPanel.transform.childCount; i++)
        {
            var ddd = JokboPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            ddd.color = Color.white;
        }
    }

    //1. 38����
    //2. ����
    //3. ��
    //4. �˸�
    //5. ����
    //6. ����
    //7. ���
    //8. ���
    //9. ����
    //10. ������
    //11. ����
    //12. ������
    //13. ��


}
