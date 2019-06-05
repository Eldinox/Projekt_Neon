using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Player")
    	{
    		collision.gameObject.GetComponent<Player>().dead = true;
            collision.gameObject.GetComponent<Player>().TakeDamage(1000);
            //Open Menu after death
            GameObject.Find("DeathScreen").GetComponent<Animator>().SetBool("death", true);
    	}
        else if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
