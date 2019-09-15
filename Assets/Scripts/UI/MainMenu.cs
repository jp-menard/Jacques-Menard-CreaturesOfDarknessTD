using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Prototype Map";

    public void NewGame()
    {
        Debug.Log("Loading Scene " + levelToLoad);
        SceneManager.LoadScene(levelToLoad);

        string path = Path.Combine(Application.persistentDataPath, "" + levelToLoad, "scene.sv");
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("New Game Started");
        }
        DirectoryInfo di2 = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "" + levelToLoad, "towerData"));
        foreach (FileInfo file in di2.GetFiles())
        {
            file.Delete();
        }
    }
    public void Continue()
    {
        Debug.Log("Loading Scene " + levelToLoad);
        SceneManager.LoadScene(levelToLoad);

    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
