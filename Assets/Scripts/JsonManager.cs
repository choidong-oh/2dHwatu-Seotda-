using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Audio;

[System.Serializable]
public class MoneyData
{
    public int PlayerMoneyData;
    public int AiMoneyData;
    public int MainPotData;
}

public class JsonManager : MonoBehaviour
{
    public MoneyData moneyData;
    private string path;
    private string fileName = "/save.txt";

    public void JsonStart()
    {
        path = Application.persistentDataPath + fileName;
        Debug.Log(path);
        LoadData();
    }

    void SaveData()
    {
        string data = JsonUtility.ToJson(moneyData);
        File.WriteAllText(path, data);
    }
    void LoadData()
    {
        if (File.Exists(path) == false)
        {
            SaveData();
        }
        string data = File.ReadAllText(path);
        moneyData = JsonUtility.FromJson<MoneyData>(data);
    }

    public void MoneySave()
    {
        moneyData.PlayerMoneyData = Player.playerMoney;
        moneyData.AiMoneyData = Ai.AiMoney;
        moneyData.MainPotData = BettingSystem.mainPot;

        SaveData();
    }
    
    public void MoneyLoad()
    {
        LoadData();
        Player.playerMoney = moneyData.PlayerMoneyData;
        Ai.AiMoney = moneyData.AiMoneyData;
        BettingSystem.mainPot = moneyData.MainPotData;

        //player°¡ ³ª°¬À»°æ¿ì aiÇÑÅ× ÆÇµ· ³Ñ°ÜÁÜ
        Ai.AiMoney += BettingSystem.mainPot;
        BettingSystem.mainPot = 0;
        if(Ai.AiMoney <= 0)
        {
            Ai.AiMoney = SceneChangeManager.instance.ChargeMoney;
        }

    }

}
