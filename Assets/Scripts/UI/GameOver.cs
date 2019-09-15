using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public string MainMenu = "Main Menu";

    void OnEnable()
    {
        roundsText.text = PlayerStats.WaveIndex.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        string path = Path.Combine(Application.persistentDataPath, "" + SceneManager.GetActiveScene().name, "scene.sv");
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("New Game Started");
        }
        DirectoryInfo di2 = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "" + SceneManager.GetActiveScene().name, "towerData"));
        foreach (FileInfo file in di2.GetFiles())
        {
            file.Delete();
        }
    }

    public void Menu()
    {
        Debug.Log("Opening main menu...");
        SceneManager.LoadScene(MainMenu);
    }
}
