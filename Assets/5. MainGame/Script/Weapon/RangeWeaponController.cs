using System.Collections;
using System.Collections.Generic;
using Script.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RangeWeaponController : MonoBehaviour
{
    private Transform _characterRangeTransform;
    private Camera mainCam;
    private Vector3 mousePosition;
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring;
    public GameObject bullet;
    private float bulletDamage;
    private Transform bulletTransform;
    private UserInput _userInput;
    private bool isTakenByPlayer = false;
    [SerializeField] private CircleCollider2D _CircleCollider2D;
    void Awake()
    {
        _characterRangeTransform = GameObject.FindGameObjectWithTag("Player").transform.Find("Weapon").transform;
        _userInput = GameObject.FindGameObjectWithTag("Player").GetComponent<UserInput>();
        bulletTransform = this.gameObject.GetComponent<Transform>();
        _CircleCollider2D = this.gameObject.GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        _CircleCollider2D.enabled =_userInput.pickWeaponPressed;
       if (isTakenByPlayer)
       {
           Shoot();
           isTakenByPlayer = !DropAndPickObject.DropWeapon(this.gameObject, this.transform.parent.gameObject);
       }
    }

           
    void Shoot()
    {
        if (canFire && _userInput.IsShoot)
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            canFire = false;
        }
        if (!canFire)
        {
            timer += Time.deltaTime;
            if ( timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Body") && !isTakenByPlayer && _characterRangeTransform.childCount == 0)
        {
            DropAndPickObject.PickWeapon(this.gameObject, col.transform.parent.transform.Find("Weapon").gameObject);            
            isTakenByPlayer = true;
        }
    }
}


