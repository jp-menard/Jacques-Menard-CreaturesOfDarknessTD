using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectUI : MonoBehaviour
{
    TowerSelectManager selectManager;

    // Start is called before the first frame update
    void Start()
    {
        selectManager = TowerSelectManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //closes UI and deselects tower
    public void Close()
    {
        selectManager.DeselectTower();
        gameObject.SetActive(false);
    }

    public void SellTower()
    {
        selectManager.SellTower();
        Close();
    }
}
