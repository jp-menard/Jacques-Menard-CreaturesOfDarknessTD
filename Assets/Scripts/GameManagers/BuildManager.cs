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
    private GameObject turretToBuild;

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
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

    //check for reasons not to build then build turret and subtract price from total gold
    public void BuildTurret(GameObject turretInstance,int price, Vector2 buildPosition)
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
                turretInstance = (GameObject)Instantiate(turretInstance, buildPosition, Quaternion.identity);
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
