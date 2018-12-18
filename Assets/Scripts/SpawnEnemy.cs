using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int maxEmemies = 20;

}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    GameManagerBehavior gameManager;
    float lastSpawnTime;
    int enemiesSpawned = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        lastSpawnTime = Time.time;
        //Instantiate(enermyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;

            // generate new enemy
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > waves[currentWave].spawnInterval) && enemiesSpawned < waves[currentWave].maxEmemies)
            {
                lastSpawnTime = Time.time;
                Instantiate(waves[currentWave].enemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }

            // generate new wave
            if (enemiesSpawned == waves[currentWave].maxEmemies && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            gameManager.isGameOver = true;
            GameObject go = GameObject.FindGameObjectWithTag("GameWon");
            go.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}
