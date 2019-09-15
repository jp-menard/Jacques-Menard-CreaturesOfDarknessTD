using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class SaveManagerV1
{
    public static void SaveScene(int wave)
    {      
        string currentScene =SceneManager.GetActiveScene().name;
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "" + currentScene));
        //deletes old data
        DirectoryInfo di1 = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "" + currentScene));
        foreach (FileInfo file in di1.GetFiles())
        {
            file.Delete();
        }
        //Save's Player data
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, ""+ currentScene,"scene.sv");

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(wave);
        formatter.Serialize(stream, data);
        stream.Close();

        //Save Tower Data
        GameObject[] towerList = GameObject.FindGameObjectsWithTag("Tower");
        //builds and populates dictionary to hold the vectors of all the tower types currently placed on map
        Dictionary<string, List<Tower>> towerDict =
            new Dictionary<string, List<Tower>>();
        foreach(GameObject tower in towerList)
        {
            Tower towerScript = tower.GetComponent<Tower>();
            string towerID = towerScript.towerID;
            List<Tower> valueList = new List<Tower>();
            if (towerDict.ContainsKey(towerID))
            {
                towerDict[towerID].Add(towerScript);
            }
            else
            {
                valueList.Add(towerScript);
                towerDict.Add(towerID, valueList);
            }
        }

        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "" + currentScene,"towerData"));
        //overwrites tower data
        DirectoryInfo di2 = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "" + currentScene, "towerData"));
        foreach (FileInfo file in di2.GetFiles())
        {
            file.Delete();
        }
        foreach (KeyValuePair<string, List<Tower>> kvp in towerDict)
        {
            formatter = new BinaryFormatter();
            path = Path.Combine(Application.persistentDataPath, "" + currentScene, "towerData",kvp.Key);
            stream = new FileStream(path, FileMode.Create);
            TowerData towerData = new TowerData(kvp.Value.ToArray());
            formatter.Serialize(stream, towerData);
            stream.Close();
        }
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
