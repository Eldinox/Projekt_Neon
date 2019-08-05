﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Enemy
{
    public float stopDistance;
    public float jumpForce;
    public GameObject thisSprite;
    public AudioClip JumperSound;
    private AudioSource JumperAudioSource;

    
    private Rigidbody2D rb;

    private float attackTime;
    private float jumpTime;
    
    public override void Start()
    {
    	base.Start();
        rb = GetComponent<Rigidbody2D>();
        JumperAudioSource = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(transform.position.x, transform.position.y, transform.position.z*Time.deltaTime);
        if(!dead && !stunned && Vector2.Distance(transform.position, player.position) < activateDistance)
        {
            if(facingRight == false && attacking == false && player.transform.position.x > transform.position.x)
            {
                Flip();
            }
            else if(facingRight == true && attacking == false && player.transform.position.x < transform.position.x)
            {
                Flip();
            }

            if(Vector2.Distance(transform.position, player.position) < stopDistance)state = "attacking";
            else state = "chasing";

            if(state == "attacking")
            {
                if(Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + attackCooldown;
                }
            }
            else if(state == "chasing")
            {
                //Debug.DrawLine(transform.position, transform.position + transform.right * spottingRange, Color.green);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                
                if(Time.time >= jumpTime)
                {
                    if(rb.velocity.y < 0.5f && rb.velocity.y > -0.5f)
                    {
                        StartCoroutine(Chase());
                        jumpTime = Time.time + 1;
                    }           
                }
            }
        }
        if(dead)
        {
            thisSprite.GetComponent<Animator>().SetTrigger("isDead");
            GetComponent<Animator>().SetTrigger("isDead");
        }
    }

    IEnumerator Chase()
    {
        float direction;
        if(player.transform.position.x < transform.position.x)direction = -.2f;
        else direction = .2f;

        rb.velocity = new Vector2(direction, 2) * jumpForce / 2;
        
        Debug.Log("Chase");

        yield return null;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);

        float direction;
        if(player.transform.position.x < transform.position.x)direction = -.2f;
        else direction = .2f;
        thisSprite.GetComponent<Animator>().SetTrigger("isAttacking");
        JumperAudioSource.clip = JumperSound;
        JumperAudioSource.Play(0);

        if(!dead && !stunned)
        {
            rb.velocity = new Vector2(direction, 2) * jumpForce;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(attacking)player.GetComponent<Player>().TakeDamage(damage);
            player.GetComponent<Player>().TakeDamage(15);
            float direction;
            if(collision.transform.position.x < transform.position.x)direction = .3f;
            else direction = -.3f;
            rb.velocity = new Vector2(direction, 2) * jumpForce / 2;
        }
    }
}
