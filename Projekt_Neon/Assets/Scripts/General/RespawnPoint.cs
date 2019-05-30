using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPoint : MonoBehaviour
{
    public GameObject fire;

    private Scene activeScene;
    
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        if(GameObject.Find("Player").GetComponent<Player>().respawnPoint == activeScene.name)
        {
            fire.SetActive(true);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<Player>().respawnPoint = activeScene.name;
            fire.SetActive(true);
        }
    }
}
