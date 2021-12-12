using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI newWaveCountdownText;

    public TextMeshProUGUI spawnMessage;
    public TextMeshProUGUI gameOverStatsText;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private bool isPaused;

    public static UI instance;

    void Awake()
    {
        instance = this;
    }

    public void UpdateHealthText(float playerHealth){
        healthText.text = "Health: " + playerHealth;
    }

    public void UpdateGoldText(float playerMoney){
        goldText.text = "Gold: " + playerMoney;
    }

    public void UpdateWaveText(float currentWave){
        waveText.text = "Wave: " + currentWave;
    }

    public void SetSpawnMessage(string message){
        spawnMessage.text = isPaused ? "" : message;
    }

    public void SetNewWaveCountdownText(string message) {
        newWaveCountdownText.text = message;
    }

    public void TogglePauseMenu(){
        isPaused = !isPaused;
        if(isPaused){
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
        }
        else {
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);
        }
    }
    
    public bool IsPaused(){
        return isPaused;
    }

    public void EndGame(int waves, int kills){
        isPaused = true;
        gameOverStatsText.text = $@"
        Waves Survived: {waves}

        Enemies Killed: {kills}
        ";
        gameOverMenu.SetActive(true);
    }


    public void RestartGame(){
        gameOverMenu.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
