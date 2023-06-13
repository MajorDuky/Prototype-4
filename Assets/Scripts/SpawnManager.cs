using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject boss;
    public GameObject[] powerupPrefabs;
    private int randomIndex;
    private int randomIndexPowerup;
    private float spawnRange;
    private float spawnPoisitionX;
    private float spawnPoisitionZ;
    public int enemyCount;
    public int waves;
    // Start is called before the first frame update
    void Start()
    {   
        spawnRange = 8f;
        waves = 1;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            
            SpawnEnemyWave(waves);
            waves++;
        }
    }
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        if (numberOfEnemies == 5 || numberOfEnemies == 10 || numberOfEnemies == 15)
        {
            foreach (var item in powerupPrefabs)
            {
                Vector3 spawnPowerupPosition = GenerateSpawnPosition(spawnRange);
                Instantiate(item, spawnPowerupPosition, item.transform.rotation);
            }
            Vector3 bossPosition = GenerateSpawnPosition(spawnRange);
            Instantiate(boss, bossPosition, boss.transform.rotation);
        }
        else
        {    
            Vector3 spawnPowerupPosition = GenerateSpawnPosition(spawnRange);
            Instantiate(powerupPrefabs[randomIndexPowerup], spawnPowerupPosition, powerupPrefabs[randomIndexPowerup].transform.rotation);

            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 spawnEnemyPosition = GenerateSpawnPosition(spawnRange);
            
                randomIndex = Random.Range(0, enemyPrefabs.Length);
                randomIndexPowerup = Random.Range(0, powerupPrefabs.Length);
                Instantiate(enemyPrefabs[randomIndex], spawnEnemyPosition, enemyPrefabs[randomIndex].transform.rotation);
            }
        }

    }

    public Vector3 GenerateSpawnPosition(float spawnRange)
    {
        spawnPoisitionX = Random.Range(-spawnRange, spawnRange);
        spawnPoisitionZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPoisitionX, 0, spawnPoisitionZ);
    }
}
