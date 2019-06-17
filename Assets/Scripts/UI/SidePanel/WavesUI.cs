using UnityEngine;
using UnityEngine.UI;

public class WavesUI : MonoBehaviour
{
    public Text wavesText;
    public Text buttonText;

    // Update is called once per frame
    void Update()
    {
        wavesText.text = "Wave " + PlayerStats.WaveIndex;
        if(GameObject.FindWithTag("Enemy") == null)
        {
            buttonText.text = ">";
        }
        else
        {
            buttonText.text = "...";
        }
        
    }
}
