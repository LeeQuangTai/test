using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthTextChange : MonoBehaviour
{
    public TextMeshProUGUI maxHealth;
    public TextMeshProUGUI currentHealth;
    public CharacterManager characterManager; 
    void Update()
    {
        maxHealth.text = characterManager._maxHP.ToString();
        currentHealth.text = characterManager._currentHP.ToString();
    }
}
