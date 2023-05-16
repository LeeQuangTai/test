using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using Script.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour, IDataSaver 
{
    public float _maxHP = 100;
    public float _currentHP { get; private set; }
    private Transform aimTransform;
    public Transform _characterWeaponTransform;
    private GameObject[] allWeapon;

    private Transform body;
    private UserInput _userInput;

    public HealthBar healthBar;
    private void Awake()
    {
        _currentHP = _maxHP;
        _userInput = this.gameObject.GetComponent<UserInput>();
        aimTransform = transform.Find("Weapon");
        _characterWeaponTransform = this.gameObject.transform.Find("Weapon").transform;
        body = this.gameObject.transform.Find("Body");
        healthBar.SetMaxHealth(_maxHP);
    }

    private void LateUpdate()
    {

        aimTransform.eulerAngles = _userInput.aimDirection;
        if (_currentHP <= 0)
        {
            Debug.Log("You died");
        }
        if (_currentHP >= _maxHP)
            _currentHP = _maxHP;
       // WeaponChangeHandler();
       if (Input.GetKeyDown(KeyCode.I))
       {
           TakeDamage(20);
       }
    }
    void WeaponChangeHandler()
    {
        if (_characterWeaponTransform.transform.childCount > 0)
        {
            if (_userInput.changeMeleeWeapon)
            {
                GetGameObject(_characterWeaponTransform, "MeleeWeapon").SetActive(true);
            }
            if (_userInput.changeRangeWeapon)
            {
                GetGameObject(_characterWeaponTransform, "RangeWeapon").SetActive(true);
            }
        }
    }

    GameObject GetGameObject(Transform _characterWeaponTransform, string weapon)
    {
        int index = -1;
        for (int i = 0; i < _characterWeaponTransform.childCount; i++)
        {
            //allWeapon[i] = _characterWeaponTransform.GetChild(i).gameObject;
            if (_characterWeaponTransform.GetChild(i).gameObject.CompareTag(weapon))
            {
                index = i;
            }
        }
        return _characterWeaponTransform.GetChild(index).gameObject;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(col.gameObject.GetComponent<EnemyAI>().damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Healer"))
        {
            HealBlood(other.gameObject.GetComponent<BloodHealer>().bloodAmount);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(float damage)
    {
        _currentHP -= damage;
        healthBar.SetHealth(_currentHP);
    }

    void HealBlood(float bloodAmount)
    {
        _currentHP += bloodAmount;
        healthBar.SetHealth(_currentHP);
    }
    public void LoadData(GameData data)
    {
        this._characterWeaponTransform = data._characterWeaponTransform;
    }

    public void SaveData(ref GameData data)
    {
        data._characterWeaponTransform = this._characterWeaponTransform;
    }  
}
