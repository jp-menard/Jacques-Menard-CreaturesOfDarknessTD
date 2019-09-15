using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerData
{
    public string towerID;
    public float[,] positions;


    public TowerData(Tower[] towers)
    {
        towerID = towers[0].towerID;
        positions = new float[towers.Length,2];

        int index = 0;
        foreach(Tower tower in towers) { 
            positions[index,0] = tower.transform.position.x;
            positions[index, 1] = tower.transform.position.y;
            index += 1;
        }
    }
}
