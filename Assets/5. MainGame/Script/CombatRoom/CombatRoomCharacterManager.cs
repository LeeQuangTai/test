using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatRoomCharacterManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "BackToWaitingRoom")
        {
            SceneManager.LoadScene("WaitingRoom");
        }
    }
}
