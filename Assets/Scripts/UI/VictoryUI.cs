using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    public Text roundsText;
    public string MainMenu = "Main Menu";

    void OnEnable()
    {
        roundsText.text = ""+ PlayerStats.WaveIndex;
    }

    public void Retry()
    {
        PlayerStats.WaveIndex = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Opening main menu...");
        SceneManager.LoadScene(MainMenu);
    }
}
