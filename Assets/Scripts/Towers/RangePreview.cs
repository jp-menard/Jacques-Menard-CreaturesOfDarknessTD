using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePreview : MonoBehaviour
{
    public SpriteRenderer RangeIndicator;
    public float range=3;
    public Color good = new Color(1, 1, 1,.1f); //white
    public Color bad= new Color(1, 0, 0,.1f);  //red

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        RangeIndicator.transform.localScale=new Vector3(range*2,range*2,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (buildManager.IsBuildable() == true)
        {
            RangeIndicator.color = good;

        }
        else
        {
            RangeIndicator.color = bad;
        }
    }
}
