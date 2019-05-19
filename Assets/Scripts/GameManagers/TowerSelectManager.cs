using UnityEngine;
using UnityEngine.UI;

public class TowerSelectManager : MonoBehaviour
{
    public static TowerSelectManager instance;

    public GameObject Indicator;
    public GameObject TowerSelectUI;

    [Header("Unity Variables")]
    public GameObject currentlySelected;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one TowerSelectManager in scene!");
            return;
        }
        instance = this;
    }

    //Called when a tower is selected on the map
    public void SelectTower(GameObject selection, Vector2 towerPosition)
    {
        currentlySelected = selection;

        Indicator.SetActive(true);
        Indicator.transform.position = towerPosition;
        
        TowerSelectUI.SetActive(true);
    }

    //Called when the tower is unselected
    public void DeselectTower()
    {
        Indicator.SetActive(false);
        currentlySelected = null;
    }

    //Sells currently selected tower
    public void SellTower()
    {
        Tower towerScript = currentlySelected.GetComponent<Tower>();
        towerScript.SellTower();
    }

    public void UpgradeTower()
    {
        Tower towerScript = currentlySelected.GetComponent<Tower>();
        towerScript.UpgradeTower();
    }
}
