using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            GameObject.Find("Player").GetComponent<Player>().respawnPoint = activeScene.name;
        }
    }
}
