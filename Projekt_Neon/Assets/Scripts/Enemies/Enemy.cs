using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Gegner Variablen")]
    public float startHealth;
    public int damage;
    public float speed;
    public float attackCooldown;
    public float activateDistance;
    public float spottingRange;
    
    public Image healthBar;
    
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public string state;

    [Header("Gegner Bools")]
    public bool facingRight = false;
    public bool attacking = false;
    public bool chasing = false;
    public bool stunned = false;

    private Animator anim ;

    private float health;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = startHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector2.Distance(transform.position, player.position);
    }

    public void TakeDamage(float damageAmount)
    {
        
    	health -= damageAmount;
        //Debug.Log(health + " / " + startHealth);
        healthBar.fillAmount = health / startHealth;
        anim.SetTrigger("getHit");
        GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = damageAmount.ToString();
        Invoke("Displaytime", 0.3f);

    	if(health <= 0)
    	{
    		//GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = "";
            Invoke("Death", 0.3f);
    	}
    }

    public void KnockDown(float duration)
    {
        stunned = true;
        //Hier code für visuellen knockdown
        GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = "down";
        Invoke("Displaytime", duration);
        Invoke("Stuntime", duration);
    }

    public void Stun(float duration)
    {
        stunned = true;
        //Hier code für visuellen stun
        GameObject.Find("DamageDisplay").GetComponent<TextMeshProUGUI>().text = "@@@";
        Invoke("Displaytime", duration);
        Invoke("Stuntime", duration);
    }

    private void Stuntime()
    {
        stunned = false;
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
