using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text goldText;

    // Update is called once per frame
    void Update()
    {
        goldText.text = "" + PlayerStats.Gold;
    }
}
