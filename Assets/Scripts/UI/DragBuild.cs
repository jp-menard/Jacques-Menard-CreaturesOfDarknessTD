using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

//Drag an preview of a tower off of a button and then create an instance of the tower at the cursor location on mouse release.
public class DragBuild : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    BuildManager buildManager;
    TowerSelectManager selectManager;

    public GameObject turretPrefab;
    public GameObject previewPrefab;
    public Button button;

    //private GameObject turretInstance;
    private GameObject previewInstance;
    private Camera cam;
    private int towerPrice;



    private void Start()
    {
        buildManager = BuildManager.instance;
        selectManager = TowerSelectManager.instance;
        buildManager.SetTowerToBuild(turretPrefab);
        cam = Camera.main;
        Tower towerScript = turretPrefab.GetComponent<Tower>();
        towerPrice = towerScript.price;
    }

    //Update is called once every frame.
    private void Update()
    {
        CheckGold();
    }

    //Checks if player can afford tower and shows preview of the tower and range under cursor
    public void OnBeginDrag(PointerEventData eventData)
    {
        selectManager.DeselectTower();
        if (PlayerStats.Gold < towerPrice)
        {
            //TODO Add display message to playerview
            Debug.Log("You don't have enough money");
            eventData.pointerDrag = null;
        }
        else {
            previewInstance = (GameObject)Instantiate(previewPrefab, ScreenToWorld(eventData.position), Quaternion.identity);
        }


    }
    
    //Updates preview's position to follow cursor
    public void OnDrag(PointerEventData eventData)
    {
        previewInstance.transform.position = ScreenToWorld(eventData.position);
    }

    //Passes turret to the buildmanager to build tower and destroys preview.
    public void OnEndDrag(PointerEventData eventData)
    {
        Tower towerScript = turretPrefab.GetComponent<Tower>();
        buildManager.BuildTower(turretPrefab,towerScript.price,ScreenToWorld(eventData.position));
        Destroy(previewInstance);
        //buildManager.SetBuildable(true);
    }
    //if player doesn't have enough gold to afford tower, set button to uninteractable.
    private void CheckGold()
    {
        if (PlayerStats.Gold < towerPrice)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
    //Calculates screen to world position of the cursor
    private Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Vector2 worldPosition = cam.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
