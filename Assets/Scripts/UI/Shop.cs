using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void BasicTurretButton()
    {
        Debug.Log("Basic Turret Clicked");
    }
    public void MissleTurretButton()
    {
        Debug.Log("Basic Turret Clicked");
    }
}
