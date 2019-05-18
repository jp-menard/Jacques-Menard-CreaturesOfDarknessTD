using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
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
}
