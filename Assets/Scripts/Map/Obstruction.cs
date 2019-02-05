using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        buildManager.SetBuildable(false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        buildManager.SetBuildable(true);
    }
}
