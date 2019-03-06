using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Player")
    	{
    		collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Player>().dead = true;
            //Open Menu after death
            GameObject.Find("DeathScreen").GetComponent<Animator>().SetBool("death", true);
    	}
    }
}
