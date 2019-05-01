using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public int lightNormalDamage;
    public int heavyNormalDamage;
    public int lightRangedDamage;
    public int heavyRangedDamage;
    public int lightStrongDamage;
    public int heavyStrongDamage;

    private Collider2D colEnemy;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Verschiedene Angriffe für verschiedene Effekte
            if(GameObject.Find("Player").GetComponent<Player>().attackState == "T1Light3")
            {
                collision.GetComponent<Enemy>().TakeDamage(lightNormalDamage * 0.75f);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Powerwave")
            {
                collision.GetComponent<Enemy>().Stun(2);
                collision.GetComponent<Enemy>().TakeDamage(lightStrongDamage);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Uppercut")
            {
                if(collision.GetComponent<Enemy>().stunned == false)
                {
                    collision.GetComponent<Enemy>().Stun(2);
                    collision.GetComponent<Rigidbody2D>().AddForce(transform.up * 3500);
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(transform.up * 2500);
                    GameObject.Find("Player").GetComponent<Player>().aircombat = true;
                }
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T3Attack" && GameObject.Find("Player").GetComponent<Player>().isGrounded == false)
            {
                collision.GetComponent<Enemy>().Stun(1);
                collision.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
                GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(transform.up * 170);
                collision.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.3f;
                colEnemy = collision;
                Invoke("Aircombat", 2);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Groundslam")
            {
                collision.GetComponent<Enemy>().Stun(2);
                collision.GetComponent<Rigidbody2D>().AddForce(-transform.up * 3500);
                collision.GetComponent<Rigidbody2D>().gravityScale = 10;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 10;
            }
            else
            {
                collision.GetComponent<Enemy>().TakeDamage(lightNormalDamage);
            }
        }
        //Unterschiedlicher Schaden für verschiedene Feinde würde hier hin kommen
        //.CompareTag("Rusher")) -> damage x2;
    }

    private void Aircombat()
    {
        colEnemy.GetComponent<Rigidbody2D>().gravityScale = 10;
        GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 10;
    }
}
