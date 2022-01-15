using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public Text waveText;
    
    void Update()
    {
        waveText.text = "wave " + PlayerStats.Rounds + " of " + PlayerStats.WavesCount;
    }
}