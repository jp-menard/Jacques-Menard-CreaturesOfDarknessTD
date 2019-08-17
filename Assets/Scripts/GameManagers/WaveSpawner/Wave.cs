using System.Collections;
using UnityEngine;

//A class that can be edited in the Unity editor to store and insantiate GameObjects with a variable delay.
public class Wave : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawn
    {
        public GameObject EnemyPrefab;
        public float Delay;
    }

    public EnemySpawn[] EnemysToSpawn;

    [HideInInspector]
    public bool spawnWaveIsRunning = false;

    public void SpawnWave(Transform spawnPoint)
    {
        if (spawnWaveIsRunning==false) {
            StartCoroutine(SpawnWaveCoroutine(spawnPoint));
        }
    }

    IEnumerator SpawnWaveCoroutine(Transform spawnPoint)
    {
        spawnWaveIsRunning = true;
        for (int i = 0; i < EnemysToSpawn.Length; i++)
        {
            SpawnEnemy(EnemysToSpawn[i].EnemyPrefab, spawnPoint);
            yield return new WaitForSeconds(.5f+EnemysToSpawn[i].Delay);
        }
        spawnWaveIsRunning = false;
    }

    void SpawnEnemy(GameObject EnemyPrefab, Transform spawnPoint)
    {
        GameObject currentSpawn = Instantiate(EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
