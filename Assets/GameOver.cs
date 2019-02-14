using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Text roundsText;

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
        //TODO Implement Main Menu Button.
        Debug.Log("Go to Menu");
    }
}
