using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMe : MonoBehaviour
{
    void Awake()
    {
        var saveMe = Object.FindObjectsOfType<SaveMe>();
        foreach (var obj in saveMe)
        {
            if (obj != this)
            {
                if (obj.name == gameObject.name)
                {
                    Destroy(gameObject);             
                }
            }
        }
        DontDestroyOnLoad(gameObject);        
    }
}
