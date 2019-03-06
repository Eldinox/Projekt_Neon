using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    public void TriggerRespawn()
    {
        player.GetComponent<Player>().RespawnAfterDeath();
    }
}
