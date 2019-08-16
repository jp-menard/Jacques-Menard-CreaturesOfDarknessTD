using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    //Saves player stats data of a given scene
    public int gold;
    public int lives;
    public int currentWave;
    public PlayerData() {
        gold=PlayerStats.Gold;
        lives=PlayerStats.Lives;
        currentWave=PlayerStats.WaveIndex;
    }
}
