using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CodeMonkey.Utils;


public class UserInput : MonoBehaviour
{
    public bool IsRun { get; private set; }
    public bool IsSit { get; private set; }
    public bool IsDash { get; private set; }
    public bool IsShoot { get; private set; }
    public bool IsMeleeAttack { get; private set; }
    public Vector2 movement { get; private set; }
    public bool changeMeleeWeapon { get; private set; }
    public bool changeRangeWeapon { get; private set; }

    public bool pickWeaponPressed { get; private set; }
    public Vector3 aimDirection { get; private set; }
    public Vector3 mousePosition{ get; private set; }
    public bool dropWeaponPressed { get; private set; }
    public (Vector3, Vector3, Vector3) aim { get; private set; }
    void Update()
    {
        IsDash = Input.GetKey(KeyCode.Space);
        IsMeleeAttack = Input.GetMouseButton(0);
        IsShoot = Input.GetMouseButton(1);
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
        changeMeleeWeapon = Input.GetKeyDown(KeyCode.Alpha1);
        changeRangeWeapon = Input.GetKeyDown(KeyCode.Alpha2);
        pickWeaponPressed = Input.GetKey(KeyCode.E);
        aimDirection = userAimDirection().Item1;
        dropWeaponPressed = Input.GetKey(KeyCode.Q);
        mousePosition = userAimDirection().Item2;
        aim = userAimDirection();
    }

    (Vector3,Vector3,Vector3) userAimDirection()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Vector3 rotation = transform.position - mousePosition;
        var rotationExpect =new Vector3(0,0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg + 90);
        //transform.rotation = Quaternion.Euler(0,0,rot + 90);
        return (new Vector3(0, 0, angle),aimDirection,rotationExpect);
    }
}