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
        PlayerStats.WaveIndex = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SaveManagerV1.SaveScene();
        Debug.Log("Opening main menu...");
        SceneManager.LoadScene(MainMenu);
    }
}
