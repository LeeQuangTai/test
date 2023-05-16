using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    private GameObject _character;
    private Rigidbody2D _rigidbody;
    private float moveSpeed;
    private bool isFacingRight = true;
    private Vector2 direction;
    private bool isMove = false;
    private Animator _animator;
    private bool isWantToHit = false;    
    private float distance;
    
    private bool isShot = false;
    private bool isTakenMeleeDamage = false;
    private float maxHP ;
    public float damage { get; private set; }
    private float firstDamage;

    private float damageHitted;
    void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        //_animator = this.GetComponent<Animator>();
        _character = GameObject.Find("Character");
    }

    private void Start()
    {
        gameObject.tag = "Enemy";
        moveSpeed = _enemyData._moveSpeed;
        maxHP = _enemyData._maxHP;
        damage = _enemyData._damage;
        firstDamage = _enemyData._damage;
    }

    void Update()
    {
        MoveTowardPlayer();
        //PlayAnimation();
        HitCharater();
        if (isShot)
        {
            maxHP -= BulletScript.BulletDamage;
            isShot = false;
            StartCoroutine(resetDamageAfterTakeDamage());
        }
        if (isTakenMeleeDamage)
        {
            maxHP -= damageHitted;
            isTakenMeleeDamage = false;
            StartCoroutine(resetDamageAfterTakeDamage());

        }
    }

    private void FixedUpdate()
    {
        if (maxHP <= 0)
        {
            Destroy(this.gameObject);
        }
    } 

    void MoveTowardPlayer()
    {
        direction = _character.transform.position - transform.position;
        transform.position =
            Vector2.MoveTowards(this.transform.position, _character.transform.position, moveSpeed * Time.deltaTime);
        isMove = true;
        //Flip();
    }
    private void Flip()
    { 
        if (isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    IEnumerator resetDamageAfterTakeDamage()
    {
        damage = 0;
        yield return new WaitForSeconds(0.5f);
        damage = firstDamage;
    }
    void HitCharater()
    {
        var distance = Math.Sqrt(Math.Pow(transform.position.x - _character.transform.position.x, 2) - Math.Pow(transform.position.y - _character.transform.position.y, 2));
        if (distance <= 1.1f)
        {
            isWantToHit = true;
            moveSpeed = 5;
        }
    }
    void PlayAnimation()
    {
        if (isMove)
        {
            _animator.Play("Walk");
        }

        if (isWantToHit)
        {
            Debug.Log("Hit player");
            isWantToHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet") ) 
        {
            isShot = true;
        }
        if (col.gameObject.CompareTag("MeleeWeapon"))
        {
            damageHitted = col.gameObject.GetComponent<MeleeWeaponController>().Weapon._damage;
            isTakenMeleeDamage = true; 
        }
    }
}
