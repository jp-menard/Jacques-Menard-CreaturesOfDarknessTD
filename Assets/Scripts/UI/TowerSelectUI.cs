using UnityEngine;
using UnityEngine.UI;

public class TowerSelectUI : MonoBehaviour
{
    public Text upgradeText;
    public Text sellText;

    TowerSelectManager selectManager;

    // Start is called before the first frame update
    void Start()
    {
        selectManager = TowerSelectManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectManager.currentlySelected != null) { 
            SetUpgradeText();
            SetSellText();
        }
        else
        {
            Close();
        }
    }

    //closes UI and deselects tower
    void Close()
    {   
            selectManager.DeselectTower();
            gameObject.SetActive(false);
    }

    public void SellTower()
    {
        selectManager.SellTower();
        Close();
    }
    
    public void UpgradeTower()
    {
        selectManager.UpgradeTower();
    }

    void SetUpgradeText()
    {
        Tower towerScript = selectManager.currentlySelected.GetComponent<Tower>();
        if (towerScript.upgradeCost != 0)
        {
            upgradeText.text = " Upgrade \n"+towerScript.upgradeCost+" G";
        }
        else
        {
            upgradeText.text = "No More Upgrades";
        }

    }

    private void SetSellText()
    {
        Tower towerScript = selectManager.currentlySelected.GetComponent<Tower>();
        sellText.text = " Sell \n"+(towerScript.sellPrice)+" G";
    }

}
