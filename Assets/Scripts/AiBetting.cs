using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBetting : MonoBehaviour
{
    public string aiBettingName;

    public void RandomAiBetting(int aiCardNum)
    {
        int maxCardValue = 20;
        float normalizedValue = (float)aiCardNum / maxCardValue;

        float foldChance = Mathf.Lerp(0.4f, 0.05f, normalizedValue);
        float callChance = Mathf.Lerp(0.3f, 0.25f, normalizedValue);
        float halfChance = Mathf.Lerp(0.2f, 0.55f, normalizedValue);
        float ppangChance = Mathf.Lerp(0.1f, 0.15f, normalizedValue);

        float rand = Random.value;

        if (rand < foldChance + callChance + halfChance)
        {
            aiBettingName = "Half";
        }
        else if (rand < foldChance + callChance)
        {
            aiBettingName = "Call";
        }
        else if (rand < foldChance)
        {
            aiBettingName = "Die";
        }
        else
        {
            aiBettingName = "Ppang";
        }
    }

    public void RandomAiBetting2()
    {
        int randomValue = Random.Range(1, 11); // 1~10 사이의 랜덤 값 생성

        if (randomValue == 1)
        {
            aiBettingName =  "Die"; 
        }
        else if (randomValue <= 4) 
        {
            aiBettingName =  "Call"; 
        }
        else if (randomValue <= 9) 
        {
            aiBettingName =  "Half"; 
        }
        else 
        {
            aiBettingName =  "Bbing"; 
        }




    }



}
