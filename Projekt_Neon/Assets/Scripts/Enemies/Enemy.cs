using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = damageAmount.ToString();
        Invoke("Displaytime", 0.3f);

    	if(health <= 0)
    	{
    		//GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = "";
            Invoke("Death", 0.3f);
    	}
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void Displaytime()
    {
        GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = "";
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
