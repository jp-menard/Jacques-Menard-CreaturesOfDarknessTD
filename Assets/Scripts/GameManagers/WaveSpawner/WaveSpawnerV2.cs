using System.Collections;
using UnityEngine;

//Instantiates waves of GameObjects that can be edited the unity Editor requires @Wave.
public class WaveSpawnerV2 : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject VictoryUI;
    public GameObject[] waves;

    [HideInInspector]
    static bool roundComplete = false;

    public void Update()
    {
        if (roundComplete == false){

            if (waves.Length > PlayerStats.WaveIndex)
            {
                Wave waveScript = waves[PlayerStats.WaveIndex].GetComponent<Wave>();
                if ((!waveScript.spawnWaveIsRunning) && (GameObject.FindWithTag("Enemy") == null))
                {
                    SaveManagerV1.SaveScene(PlayerStats.WaveIndex);
                    roundComplete = true;
                }

            }
            else
            {
                if (GameObject.FindWithTag("Enemy") == null)
                {
                    VictoryUI.SetActive(true);
                }
            }
        }
        
    }

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
            roundComplete = false;
        }
        /*else
        {
            VictoryUI.SetActive(true);
        }
        */
    }

}
