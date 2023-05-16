using System.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GoToRoomManager : MonoBehaviour
{
    CameraFollow mainCamera;
    GameObject[] weapon;
    Transform characterWeaponTransform; 
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        characterWeaponTransform  = GameObject.FindGameObjectWithTag("Player").transform.Find("Weapon").transform;
    }

    private void OnTriggerEnter2D(Collider2D col)   
    {
        if (col.gameObject.name == "Shop")
        {
            setPosition(new Vector2(19f, 27f),CameraFollow._minMaxPosionInShop.Item1,CameraFollow._minMaxPosionInShop.Item2);
        }

        if (col.gameObject.name == "PracticeRoom")
        {
            setPosition(new Vector2(-24f, -27.2f),CameraFollow._minMaxPosionInPractice.Item1,CameraFollow._minMaxPosionInPractice.Item2);
        }

        if (col.gameObject.name == "PracticeToWaitingTrigger")
        {
            setPosition(new Vector2(-13.65f, -10.3f),CameraFollow._minAndMaxPositionInWaitingRoom.Item1,CameraFollow._minAndMaxPositionInWaitingRoom.Item2);
        }

        if (col.gameObject.name == "ShopToWaitingTrigger")
        {
            setPosition(new Vector2(10f, 5.8f), CameraFollow._minAndMaxPositionInWaitingRoom.Item1,CameraFollow._minAndMaxPositionInWaitingRoom.Item2);
        }

        if (col.gameObject.name == "BackToWaitingRoom")
        {
            SceneManager.LoadScene("WaitingRoom");
            this.transform.position = new Vector2(15.64f,0);
        }
    
        if (col.gameObject.name == "EscapeTrigger")
        {
            SceneManager.LoadScene("MainGame");
            this.transform.position = new Vector2(-28,0);
            mainCamera.minPos = CameraFollow._minMaxPositionInCombat.Item1;
            mainCamera.maxPos = CameraFollow._minMaxPositionInCombat.Item2;
            
        }
    }
    void setPosition(Vector2 characterPosition, Vector2 cameraMinPos, Vector2 cameraMaxPos)
    {
        gameObject.transform.position = characterPosition;
        mainCamera.minPos = cameraMinPos;
        mainCamera.maxPos = cameraMaxPos;
    }
}
