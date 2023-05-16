using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Numerics;
using CodeMonkey.Utils;
using UnityEditor.UIElements;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BulletScript : MonoBehaviour
{
    private float topLimit = 30f;
    private float leftLimit = -40f;
    private float rightLimit = 40f;
    private float bottomLimit = -30f;
    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float force = 40;

    public static float BulletDamage = 5;

    public RangeWeapon RangeWeapon; 
    // Start is called before the first frame update
    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Start()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        BulletDamage = RangeWeapon.damage;
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot + 90);
    }

    void Update()
    {
        //destroyThis();
    }

    void destroyThis()
    {
        if (transform.position.x <= bottomLimit || transform.position.x >= topLimit || transform.position.y >= rightLimit || transform.position.y <= leftLimit)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        
        if (col.gameObject.CompareTag("MapObject"))
        {
            Destroy(this.gameObject );
        }

        if (col.gameObject.CompareTag("Body"))
        {
            Destroy(this.gameObject);
        }
    }
}
    