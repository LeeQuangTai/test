using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveChangeText : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TextMeshProUGUI waveID;
    private void Update()
    {
        waveID.text = spawnManager.currentWave.ToString();
    }
}
