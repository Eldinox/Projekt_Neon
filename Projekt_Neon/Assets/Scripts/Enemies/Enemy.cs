using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float attackCooldown;
    public float activateDistance;
    public float spottingRange;
    
    
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public string state;

    public bool facingRight = false;
    public bool attacking = false;
    public bool chasing = false;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector2.Distance(transform.position, player.position);
    }

    public void TakeDamage(int damageAmount)
    {
    	health -= damageAmount;

    	if(health <= 0)
    	{
    		Destroy(this.gameObject);
    	}
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
