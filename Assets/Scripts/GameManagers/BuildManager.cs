using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject[] towerPrefabs;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        //load towers for the level
        string currentScene = SceneManager.GetActiveScene().name;
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "" + currentScene));
        DirectoryInfo di1 = new DirectoryInfo(Path.Combine(Application.persistentDataPath,currentScene, "towerData"));

        foreach(FileInfo file in di1.GetFiles()) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = file.FullName;
            FileStream stream = new FileStream(path, FileMode.Open);
            TowerData data = formatter.Deserialize(stream) as TowerData;
            stream.Close();
            for(int i = 0; i < data.positions.GetLength(0); i++)
            {
                Vector2 vector = new Vector2(data.positions[i,0],data.positions[i,1]);
                BuildByID(data.towerID, vector);
            }
        }
    }
    private bool buildable = true;
    private GameObject towerToBuild;

    public void SetTowerToBuild(GameObject tower)
    {
        towerToBuild = tower;
    }
    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }
    
    public bool IsBuildable()
    {
        if (buildable == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //changes buildable state when encountering obstructions
    public void SetBuildable(bool buildState)
    {
        buildable = buildState;
    }

    //check for reasons not to build then build tower and subtract price from total gold
    public void BuildTower(GameObject towerInstance,int price, Vector2 buildPosition)
    {

        if (buildable)
            {
            if (PlayerStats.Gold >= price)
            {
                PlayerStats.Gold -= price;
            }
            else
            {
                //not enough money to build tower skip build checks
                Debug.Log("Didn't Build due to lack of gold");
                return;
            }
                towerInstance = (GameObject)Instantiate(towerInstance, buildPosition, Quaternion.identity);
            //TODO Display on screen!
            Debug.Log("Tower Built");
        }
        else {
            //TODO Display on screen!
            Debug.Log("Can't Build Here!");
            return;
        }
    }

    public GameObject BuildByID(string towerID, Vector2 buildPosition)
    {
        GameObject towerInstance=null;
        foreach (var towerPrefab in towerPrefabs)
        {
            Tower towerScript = towerPrefab.GetComponent<Tower>();
            if (towerScript.towerID.Equals(towerID))
            {
                towerInstance = (GameObject)Instantiate(towerPrefab, buildPosition, Quaternion.identity);
                return towerInstance;
            }
        }
        return towerInstance;
    }
}
