using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float playerMoney = 100.0f;
    public float playerHealth = 100.0f;
    public int currentWave = 1;
    public float paymentRate = 1.0f;
    public float lastPaymentTime;
    public float paymentAmmount = 10.0f;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UI.instance.UpdateHealthText(playerHealth);
        UI.instance.UpdateGoldText(playerMoney);
        UI.instance.UpdateWaveText(currentWave);

        if (CanGiveGold())
        {
            GiveGold();
        }
        if(playerHealth <= 0){
            UI.instance.EndGame(WaveManager.instance.waveNum, WaveManager.instance.enemiesKilled);
        }
    }


    public bool CanGiveGold()
    {
        if (Time.time - lastPaymentTime >= paymentRate)
        {
            return true;
        }
        return false;
    }

    public void GiveGold()
    {
        lastPaymentTime = Time.time;
        playerMoney += paymentAmmount;
    }

    public void UpdateGold(float ammount)
    {
        playerMoney += ammount;
        UI.instance.UpdateGoldText(playerMoney);
    }

    public void UpdateHealth(float ammount) {
        playerHealth += ammount;
        UI.instance.UpdateHealthText(playerHealth);
    }

    public void UpdateWave(int ammount)
    {
        currentWave += ammount;
        UI.instance.UpdateWaveText(currentWave);
    }

}
