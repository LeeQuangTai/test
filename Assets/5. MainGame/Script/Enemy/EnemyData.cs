using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(fileName = "New enemy data", menuName = "Enemy Data") ]
public class EnemyData : ScriptableObject
{
    public float _maxHP;
    public float _damage;
    public float _moveSpeed;
    public int _level;
    public int typeMonter;// 1: Melee monster 2: Range monster
}
