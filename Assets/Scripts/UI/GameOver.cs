using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public string MainMenu = "Main Menu";

    void OnEnable()
    {
        roundsText.text = PlayerStats.Waves.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Opening main menu...");
        SceneManager.LoadScene(MainMenu);
    }
}
