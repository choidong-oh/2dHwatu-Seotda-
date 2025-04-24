using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public enum JokboType
{
    SamPalGwangDdaeng = 1,  // »ïÆÈ±¤¶¯
    GwangDdaeng,            // ±¤¶¯
    Ddaeng,                 // ¶¯
    Ali,                    // ¾Ë¸®
    Doksa,                  // µ¶»ç
    GuPping,                // ±¸»æ
    JangPping,              // Àå»æ
    JangSa,                 // Àå»ç
    SeRuk,                  // ¼¼·ú
    DdaengJabi,             // ¶¯ÀâÀÌ
    GuSa,                   // ±¸»ç
    AmhaengEosa,            // ¾ÏÇà¾î»ç
    Ggut                    // ²ý
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

    //1. 38±¤‹¯
    //2. ±¤¶¯
    //3. ¶¯
    //4. ¾Ë¸®
    //5. µ¶»ç
    //6. ±¸»æ
    //7. Àå»æ
    //8. Àå»ç
    //9. ¼¼·ú
    //10. ¶¯ÀâÀÌ
    //11. ±¸»ç
    //12. ¾ÏÇà¾î»ç
    //13. ²ý


}
