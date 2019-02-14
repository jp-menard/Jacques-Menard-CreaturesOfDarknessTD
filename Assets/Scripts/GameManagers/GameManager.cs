using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool GameIsOver;
    // Start is called before the first frame update
    public GameObject gameOverUI;

    private void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
        {
            return;
        }
        //Debug end game
        if(Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
