using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(instance); }
    }

    [SerializeField] TextMeshProUGUI PlayerMoney;
    [SerializeField] JsonManager jsonManager;

    public int ChargeMoney = 1000000;

    private void Start()
    {
        jsonManager.JsonStart();    
        jsonManager.MoneyLoad();
    }
    private void Update()
    {
        PlayerMoney.text = Player.playerMoney.ToString();
    }
    public void SceneChange(string SceneName)
    {
        if(SceneName == "1")
        {
            if(Player.playerMoney == 0)
            {
                return;
            }
        }
        SceneManager.LoadScene(SceneName);
    }

    public void Charge(int _chargeMoney)
    {
        if(Player.playerMoney > _chargeMoney)
        {
            return ;
        }
        Player.playerMoney = _chargeMoney;
        //Ai.AiMoney = _chargeMoney;
        BettingSystem.mainPot = 0;

        jsonManager.MoneySave();

    }

    public void ResetMoney(int _chargeMoney)
    {
        Player.playerMoney = _chargeMoney;
        Ai.AiMoney = _chargeMoney;
        BettingSystem.mainPot = 0;

        jsonManager.MoneySave();
    }

}
