using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private float smooth = 0.1f;
    public Vector2 maxPos;//= new Vector2(4, 4.22f);
    public Vector2 minPos;//= new Vector2(-4,-4.22f);

    public static (Vector2, Vector2) _minAndMaxPositionInWaitingRoom = (new Vector2(-4,-4.22f),new Vector2(4.5f, 0)) ;
    public static (Vector2, Vector2) _minMaxPosionInPractice         = (new Vector2(-50f, -29),new Vector2(-35f, -24.5f)) ;
    public static (Vector2, Vector2) _minMaxPosionInShop             = (new Vector2(27, 21),   new Vector2(38.4f, 29));
    public static (Vector2, Vector2) _minMaxPositionInCombat         = (new Vector2(-19.9f, -11.5f),new Vector2(15.5f, 12));
    

    private void Awake()
    {
        minPos = _minAndMaxPositionInWaitingRoom.Item1;
        maxPos = _minAndMaxPositionInWaitingRoom.Item2;
        target = GameObject.Find("Character").transform;
    }

    private void Start()
    {
        // maxPos = new Vector2(wallLimitLeft.position.x, wallLimitTop.position.y);
        // minPos = new Vector2(wallLimitRight.position.x, wallLimitBottom.position.y);
    }

    void Update()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition,smooth) + new Vector3(0,0,-10);
        }
    }
}
