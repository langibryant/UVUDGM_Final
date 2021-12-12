using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject tankEnemyPrefab;

    public float timeToNextWave = 5.0f;

    private float countdown = 5.0f;

    private float spawnCooldown = 0.4f;

    public int waveNum = 0;

    public int enemiesKilled = 0;

    public Transform spawnPoint;

    public static WaveManager instance;

    public int enemyHealthBonus = 0;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(!UI.instance.IsPaused()){
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Enemy");
            if(countdown <= 0.0f) {
                StartCoroutine(SpawnWave());
                UI.instance.SetNewWaveCountdownText("");
                countdown = timeToNextWave;
            }
            if(gos.Length == 0){
                countdown -= Time.deltaTime;
            }
            UI.instance.SetNewWaveCountdownText(gos.Length == 0 ? Math.Round(countdown, 2).ToString() : "");
        }
    }

    IEnumerator SpawnWave() {
        waveNum ++;
        if(waveNum % 3 == 0) {
            spawnCooldown = spawnCooldown / 0.75f; 
            enemyHealthBonus += 50;
        }
        GameManager.instance.UpdateWave(1);
        for(int i = 0; i < waveNum * waveNum + 5; i++){
            SpawnEnemy();
            if(spawnCooldown > .1){
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    void SpawnEnemy() {
        System.Random rd = new System.Random();

        double rand_num = rd.Next(0, 100);
        if(waveNum < 3) {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else if(waveNum > 3 && waveNum < 6) {
            if (rand_num > 40) {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else if (rand_num < 40 && rand_num > 0) {
                GameObject newEnemy = Instantiate(fastEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else {
            if (rand_num > 60){
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else if (rand_num < 60 && rand_num > 20) {

                GameObject newEnemy = Instantiate(fastEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else if(rand_num < 20 && rand_num > 0){
                GameObject newEnemy = Instantiate(tankEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    public void IncrementEnemiesKilled(){
        enemiesKilled ++;
    }
}
