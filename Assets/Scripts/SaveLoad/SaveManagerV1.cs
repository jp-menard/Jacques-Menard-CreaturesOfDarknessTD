using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveManagerV1
{
    
    public static void SaveScene()
    {
        string currentScene =SceneManager.GetActiveScene().name;
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "" + currentScene));
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, ""+ currentScene,"scene.sv");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Autosave Complete");
    }

    //requires the name of the scene to load
    public static PlayerData LoadScene(string currentScene)
    {
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "" + currentScene));
        string path = Path.Combine(Application.persistentDataPath, "" + currentScene, "scene.sv");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
