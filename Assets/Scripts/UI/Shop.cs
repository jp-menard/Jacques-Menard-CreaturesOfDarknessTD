using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void BasicTowerButton()
    {
        Debug.Log("Basic Turret Clicked");
    }
    public void MissleTowerButton()
    {
        Debug.Log("Basic Turret Clicked");
    }
    public void MageTowerButton()
    {
        Debug.Log("Basic Turret Clicked");
    }
}
