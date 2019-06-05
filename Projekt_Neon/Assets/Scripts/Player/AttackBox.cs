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
            //Normalform
            if(GameObject.Find("Player").GetComponent<Player>().attackState == "T1Light")
            {
                collision.GetComponent<Enemy>().TakeDamage(lightNormalDamage);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T1Heavy")
            {
                collision.GetComponent<Enemy>().TakeDamage(heavyNormalDamage);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T1Light3")
            {
                collision.GetComponent<Enemy>().TakeDamage(lightNormalDamage * 0.75f);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T1Big")
            {
                collision.GetComponent<Enemy>().TakeDamage(lightNormalDamage * 1.75f);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Weit")
            {
                collision.GetComponent<Enemy>().TakeDamage(40);
            }
            //Strongform
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T2Light")
            {
                collision.GetComponent<Enemy>().TakeDamage(lightStrongDamage);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T2Heavy")
            {
                collision.GetComponent<Enemy>().TakeDamage(heavyStrongDamage);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Groundsmash")
            {
                collision.GetComponent<Enemy>().Stun(3);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Powerwave")
            {
                collision.GetComponent<Enemy>().Stun(3);
                collision.GetComponent<Enemy>().TakeDamage(35);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T3Heavy3")
            {
                collision.GetComponent<Enemy>().TakeDamage(45);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Overhead")
            {
                collision.GetComponent<Enemy>().Stun(3);
                collision.GetComponent<Enemy>().TakeDamage(50);
            }
            //Rangedform
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Uppercut")
            {
                if(collision.GetComponent<Enemy>().stunned == false)
                {
                    if(collision.gameObject.name != "Jumper" && collision.gameObject.name != "Jumper(Clone)")
                    {
                        collision.GetComponent<Enemy>().Stun(3);
                        collision.GetComponent<Rigidbody2D>().AddForce(transform.up * 3500);
                        GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(transform.up * 2500);
                        GameObject.Find("Player").GetComponent<Player>().aircombat = true;
                    }
                }
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T3Light" && GameObject.Find("Player").GetComponent<Player>().isGrounded == false || GameObject.Find("Player").GetComponent<Player>().attackState == "T3Heavy" && GameObject.Find("Player").GetComponent<Player>().isGrounded == false)
            {
                collision.GetComponent<Enemy>().Stun(1);
                collision.GetComponent<Enemy>().TakeDamage(lightRangedDamage);
                collision.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
                GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(transform.up * 170);
                collision.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.3f;
                colEnemy = collision;
                Invoke("Aircombat", 2);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "Groundslam")
            {
                collision.GetComponent<Enemy>().Stun(3);
                collision.GetComponent<Rigidbody2D>().AddForce(-transform.up * 3500);
                collision.GetComponent<Rigidbody2D>().gravityScale = 10;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 10;
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T3Light")
            {
                collision.GetComponent<Enemy>().TakeDamage(lightRangedDamage);
            }
            else if(GameObject.Find("Player").GetComponent<Player>().attackState == "T3Heavy")
            {
                collision.GetComponent<Enemy>().TakeDamage(heavyRangedDamage);
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
