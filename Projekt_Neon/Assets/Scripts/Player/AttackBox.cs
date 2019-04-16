using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Verschiedene Angriffe für verschiedene Effekte
            if(GameObject.Find("Player").GetComponent<Player>().attackState == "Powerwave")
            {
                collision.GetComponent<Enemy>().Stun(5);
            }
            else
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        //Unterschiedlicher Schaden für verschiedene Feinde würde hier hin kommen
        //.CompareTag("Rusher")) -> damage x2;
    }
}
