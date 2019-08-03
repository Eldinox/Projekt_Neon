using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackBoxBoss : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         if (col.gameObject.tag == "Player")
        {
            Debug.Log("check collision boss player");
            player.GetComponent<Player>().TakeDamage(15);

        }
    }
}
