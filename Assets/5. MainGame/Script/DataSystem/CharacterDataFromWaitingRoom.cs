using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterDataFromWaitingRoom: MonoBehaviour
{
    public static float maxHP;
    public static List<Transform> weapon;
    public static float _baseDamage;
    public static float _baseMoveSpeed;
    public static float _baseAttackSpeed;

    public static CharacterDataFromWaitingRoom instance { get; private set;  }
}
