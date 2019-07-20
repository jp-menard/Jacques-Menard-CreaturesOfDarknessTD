using UnityEngine;
using UnityEngine.UI;

public class TargetingMethodUI : MonoBehaviour
{
    public Text TargetingMethodText;
    TowerSelectManager selectManager;
    public string[] targetingMethods=new string[] {"TargetFirst","TargetLast","TargetClosest","TargetStrong" };
    private int currTargetingMethod;
    // Start is called before the first frame update
    void Start()
    {

        selectManager = TowerSelectManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectManager.currentlySelected != null) { 
            Tower towerScript = selectManager.currentlySelected.GetComponent<Tower>();
            TargetingMethodText.text = towerScript.targetMethod;
            currTargetingMethod = System.Array.IndexOf(targetingMethods,towerScript.targetMethod);
        }
    }
    
    public void NextTargetingMethod()
    {
        Tower towerScript = selectManager.currentlySelected.GetComponent<Tower>();
        if (currTargetingMethod >= targetingMethods.Length - 1)
        {
            
            currTargetingMethod = 0;
            
        }
        else
        {
            currTargetingMethod += 1;
        }
        towerScript.targetMethod = targetingMethods[currTargetingMethod];
    }

    public void PrevTargetingMethod()
    {
        Tower towerScript = selectManager.currentlySelected.GetComponent<Tower>();
        if (currTargetingMethod <= 0)
        {

            currTargetingMethod = targetingMethods.Length - 1;

        }
        else
        {
            currTargetingMethod -= 1;
        }
        towerScript.targetMethod = targetingMethods[currTargetingMethod];
    }

}
