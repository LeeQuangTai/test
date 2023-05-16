using System;
using System.Collections;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        private Rigidbody2D _rigidbody2D;
        private bool isFacingRight = true;
        private bool isSit;
        private bool canDash = true;
        private bool isDashing;
        private float dashingPower = 30f;
        private float dashingTime = 0.1f;
        private float dashingCoolDown = 0.5f;
        private UserInput _userInput;

        private bool canGoThrough = false;

        // Start is called before the first frame update
        void Awake()
        {
            _rigidbody2D = this.GetComponent<Rigidbody2D>();
            _userInput = GameObject.FindGameObjectWithTag("Player").GetComponent<UserInput>();
        }
        private void FixedUpdate()
        {
            StartCoroutine(Dash());
            OnMove(_userInput.movement);
            //Flip(movement);
        }

        private void OnMove(Vector2 movement)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + movement * (moveSpeed * Time.fixedDeltaTime));
            //Flip();
        }

        void Flip(Vector2 movement)
        {
            if (isFacingRight && movement.x > 0f || !isFacingRight && movement.x < 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

        private IEnumerator Dash()
        {
            if (_userInput.IsDash && canDash) 
            {
                canDash = false;
                isDashing = true;
                canGoThrough = true;
                moveSpeed = dashingPower;
                //_rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
                //_rigidbody2D.MovePosition(_rigidbody2D.position + movement * (dashingTime * Time.fixedDeltaTime));
                yield return new WaitForSeconds(dashingTime);
                isDashing = false;
                moveSpeed = 10;
                canGoThrough = false;
                yield return new WaitForSeconds(dashingCoolDown);
                canDash = true;
            }
        }
    }
}
