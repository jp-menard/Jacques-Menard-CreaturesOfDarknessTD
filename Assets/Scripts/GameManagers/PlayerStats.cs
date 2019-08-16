using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int Gold;
    public int startGold = 200;

    public static int Lives;
    public int startLives = 5;

    public static int WaveIndex;

    private void Start()
    {

        string path = Path.Combine(Application.persistentDataPath, "" + SceneManager.GetActiveScene().name, "scene.sv");
        if (File.Exists(path))
        {
            PlayerStats.LoadPlayerStats(SaveManagerV1.LoadScene(SceneManager.GetActiveScene().name));
        }
        else { 
            Gold = startGold;
            Lives = startLives;
            WaveIndex = 0;
        }

    }
    public static void LoadPlayerStats(PlayerData data)
    {
        Debug.Log("Save Loaded");
        Gold = data.gold;
        Lives = data.lives;
        WaveIndex = data.currentWave;
    }
    public static void TakeLives(int damage)
    {
        if (Lives - damage > 0)
        {
            Lives -= damage;
        }else
        {
            Lives = 0;
        }

    }
}
