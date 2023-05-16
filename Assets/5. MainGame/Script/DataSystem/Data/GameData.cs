using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]

public class GameData
{
    // Idea: this class is used to save what weapon the character choose in the waiting room;    
    [FormerlySerializedAs("weapon")] [CanBeNull] public Transform _characterWeaponTransform;   
    public GameData()   
    {   
        this._characterWeaponTransform = null;
    }
}
