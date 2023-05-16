using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool isIdle = true;
    private bool isRun = false;
    private bool isSit = false;
    private bool isJump = false;
    private bool isMeleeAttack = false;
    private bool isShoot = false;

    private int attack;
    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.Find("Pirate_01-Rig").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
    }
    private void FixedUpdate()
    {
        if (isIdle)
        {
            OnIdle();
        }
        if (isJump)
        {
            OnJump();
        }
        if (isRun)
        {
            OnRun();
        }
        if (isSit)
        {
            OnSit();
        }
        if (isMeleeAttack)
        {
            OnAttack(attack);
        }

        if (isShoot)
        {
            OnAttack(attack);
        }
        
    }

    private void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.C))  //아래 버튼 눌렀을때. 
        {
            isSit = true;
            isIdle = false;
            isJump = false;
            isRun = false;

        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isSit = false;
            isIdle = true;
            isJump = false;
            isRun = false;
        }
        else if (UnityEngine.Input.GetKeyDown(KeyCode.A) ||
                 UnityEngine.Input.GetKeyDown(KeyCode.D) ||
                 UnityEngine.Input.GetKeyDown(KeyCode.W) ||
                 UnityEngine.Input.GetKeyDown(KeyCode.S))
        {
            //OnRun();
            isRun = true;
            isSit = false;
            isIdle = false;
            isJump = false;
            isMeleeAttack = false; 
        }
        
        else if (UnityEngine.Input.GetKeyUp(KeyCode.A) ||
                 UnityEngine.Input.GetKeyUp(KeyCode.D) ||
                 UnityEngine.Input.GetKeyUp(KeyCode.W) ||
                 UnityEngine.Input.GetKeyUp(KeyCode.S))
        {
            isRun = false;
            isIdle = true;
            isSit = false;
            isMeleeAttack = false; 
            isJump = false;
        }
        else if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            isIdle = false;
            isSit = false;
            isRun = false;
            isMeleeAttack = false; 

            //OnJump();
        } else if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
        {
            isJump = false;
            isIdle = true;
            isSit = false;
            isRun = false;
            isMeleeAttack = false; 
        }
        else if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            isMeleeAttack = true;
            attack = 1;
            isJump = false;
            isIdle = false;
            isSit = false;
            isRun = false;
        } else if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            isMeleeAttack = false;    
        }
        else if ((UnityEngine.Input.GetMouseButtonDown(1)))
        {
            isMeleeAttack = true;
            attack = 2;
            isJump = false;
            isIdle = false;
            isSit = false;
            isRun = false;
        }
        else if ((UnityEngine.Input.GetMouseButtonUp(1)))
        {
        }

    }

    private void OnIdle()
    {
        _animator.Play("Pirat01_Idle");
    }

    private void OnRun()
    {
        _animator.Play("Pirat01_Walking");
    }
    private void OnSit()
    {
        _animator.Play("Sit");
    }

    private void OnJump()
    {
        _animator.Play("Pirat01_Jump");
    }

    private void OnAttack(int attack)
    {
        if (attack == 1 && isMeleeAttack)
        {
            _animator.Play("Pirat01_Attack-Sable");

        }
        else _animator.Play("Pirat01_Attack-Pistol");
        isMeleeAttack = false;
    }
}
