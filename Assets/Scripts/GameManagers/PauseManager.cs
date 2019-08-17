using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject ui;
    public string MainMenu = "Main Menu";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }
    //Pauses the game and activates the pause menu.
    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        
        if (ui.activeSelf == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        string path = Path.Combine(Application.persistentDataPath, "" + SceneManager.GetActiveScene().name, "scene.sv");
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("New Game Started");
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SaveManagerV1.SaveScene(PlayerStats.WaveIndex);
        Debug.Log("Opening main menu...");
        SceneManager.LoadScene(MainMenu);
    }
}
