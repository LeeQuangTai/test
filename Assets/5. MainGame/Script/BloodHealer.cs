using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.AI;


public class BloodHealer : MonoBehaviour
{
    public float bloodAmount = 5;

    private void Start()
    {
        StartCoroutine(DestroyBlood());
    }

    IEnumerator DestroyBlood()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
