using System;
using System.Collections;
using System.Collections.Generic;
using Script.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeWeaponController : MonoBehaviour
{
    private bool _canPickUp;
    private Transform _characterMeleeTransform;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody;
    private UserInput _userInput;
    private SpriteRenderer HitBoxEffect;
    public MeleeWeapon Weapon;
    public bool isTakenByPlayer = false;
    private void Awake()
    {
        _characterMeleeTransform = GameObject.FindGameObjectWithTag("Player").transform.Find("Weapon").transform;
        HitBoxEffect = gameObject.transform.Find("HitBoxDisplay").gameObject.GetComponent<SpriteRenderer>();
        _userInput = GameObject.FindGameObjectWithTag("Player").GetComponent<UserInput>();
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    } 

    void Start()
    {
        _rigidbody.gravityScale = 0;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _boxCollider2D.isTrigger = true;
        _boxCollider2D.enabled = false;
    }

    void Update()
    {
        _boxCollider2D.enabled = _userInput.pickWeaponPressed;
        if (isTakenByPlayer)
        {
            HitBoxEffect.enabled = _boxCollider2D.enabled = _userInput.IsMeleeAttack;
            isTakenByPlayer  = !DropAndPickObject.DropWeapon(this.gameObject,this.gameObject.transform.parent.gameObject);
            //isTakenByPlayer = this.gameObject.transform.IsChildOf(_characterMeleeTransform);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Body") && !isTakenByPlayer && _characterMeleeTransform.childCount == 0)  
        {
            DropAndPickObject.PickWeapon( this.gameObject, col.transform.parent.transform.Find("Weapon").gameObject);
            isTakenByPlayer = true;
        }
    }
}
