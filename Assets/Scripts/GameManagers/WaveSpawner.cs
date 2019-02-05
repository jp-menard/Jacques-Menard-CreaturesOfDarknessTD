using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 2f;
    public float spawnDelay = .2f;

    public int numWaves = 10;

    private int waveIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0f)
        {
            if (numWaves >= 0)
            {
                StartCoroutine(SpawnWave());
                numWaves--;
                
            }
            countdown = timeBetweenWaves;
  
        }

        countdown -= Time.deltaTime;
        
    }
    
    //
    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    
}
