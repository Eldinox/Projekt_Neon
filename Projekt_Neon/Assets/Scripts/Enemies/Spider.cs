using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public float stopDistance;
    public GameObject projectile;
    public Transform shotPoint;

    private float attackTime;

    // Start is called before the first frame update
    public override void Start()
    {
    	base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
        	if(Vector2.Distance(transform.position, player.position) > stopDistance)
        	{
        		
        	}
            else
            {
                if(Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + attackCooldown;
                }
            }
        }
        if(facingRight == false && player.transform.position.x > transform.position.x)
        {
            Flip();
        }
        else if(facingRight == true && player.transform.position.x < transform.position.x)
        {
            Flip();
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(projectile, shotPoint.position, transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
