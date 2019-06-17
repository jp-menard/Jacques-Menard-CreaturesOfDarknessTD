using System.Collections;
using UnityEngine;

//Instantiates waves of GameObjects that can be edited the unity Editor requires @Wave.
public class WaveSpawnerV2 : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject VictoryUI;
    public GameObject[] waves;
    

    //Calls a subroutine to spawn waves in order
    public void SpawnNextWave()
    {
        //checks if round is over
        if (GameObject.FindWithTag("Enemy")!=null) {
            Debug.Log("Round isn't over!");
            return;
        }
        if (waves.Length > PlayerStats.WaveIndex) {
            Wave waveScript = waves[PlayerStats.WaveIndex].GetComponent<Wave>();
            waveScript.SpawnWave(spawnPoint);
            PlayerStats.WaveIndex++;
        }
        else
        {
            VictoryUI.SetActive(true);
        }
    }

}
