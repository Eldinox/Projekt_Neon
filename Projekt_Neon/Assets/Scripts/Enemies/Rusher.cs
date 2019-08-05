using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher : Enemy
{
    public float stopDistance;
    //public float attackSpeed;
    public float dashSpeed;
    public float knockbackForce;
    public Transform[] patrolSpots;
    public float startWaitTime;
    public AudioClip RusherSound;
    private AudioSource RusherAudioSource;

    private float attackTime;
    private Rigidbody2D rb;
    private int direction;
    private int randomSpot;
    private float waitTime;
    private Animator anim;


    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        randomSpot = Random.Range(0, patrolSpots.Length);
        waitTime = startWaitTime;
        anim = GetComponent<Animator>();
        RusherAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead && Vector2.Distance(transform.position, player.position) < activateDistance && !stunned)
        {
            if(facingRight == false && attacking == false && player.transform.position.x > transform.position.x)
            {
                Flip();
            }
            else if(facingRight == true && attacking == false && player.transform.position.x < transform.position.x)
            {
                Flip();
            }
            
            if(facingRight)direction = -1;
            else direction = 1;
            RaycastHit2D vision = Physics2D.Raycast(transform.position, transform.right * direction, spottingRange);
            
            if(Vector2.Distance(transform.position, player.position) < stopDistance)state = "attacking";
            else if(vision.collider != null && vision.collider.CompareTag("Player"))state = "chasing";
            else state = "patrolling";


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
                Debug.Log("isChasing");
                Debug.DrawLine(transform.position, transform.position + transform.right * spottingRange, Color.green);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isWalking",true);
                Debug.Log("isWalking");
                Debug.DrawLine(transform.position, transform.position + transform.right * spottingRange, Color.green);
                transform.position = Vector2.MoveTowards(transform.position, patrolSpots[randomSpot].position, speed/4 * Time.deltaTime);
                if(patrolSpots[randomSpot].position.x > transform.position.x && facingRight == false)Flip();
                else if(patrolSpots[randomSpot].position.x < transform.position.x && facingRight == true)Flip();

                if(Vector2.Distance(transform.position, patrolSpots[randomSpot].position) < 3)
                {
                    if(waitTime <= 0)
                    {
                        randomSpot = Random.Range(0, patrolSpots.Length);
                        waitTime = startWaitTime;
                    }
                    else
                    {
                       
                        waitTime -= Time.deltaTime;
                    }
                     anim.SetBool("isWalking",false);
                }
            }
        }
        if (dead)
        {
            Debug.Log("dead");
            anim.SetTrigger("isDead");
        }
    }

    IEnumerator Attack()
    {
        
        attacking = true;
        int direction = 0;
        if(player.transform.position.x < transform.position.x)direction = -1;
        else direction = 1;
        anim.SetBool("isAttacking",true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isAttacking",false);
        if(!dead && !stunned) 
        {
            rb.velocity = new Vector2(direction, 0) * dashSpeed;
            RusherAudioSource.clip = RusherSound;
            RusherAudioSource.Play(0);
           
        }
        yield return new WaitForSeconds(.5f);
        attacking = false;
    }
    IEnumerator Chase()
    {
        chasing = true;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        yield return null;
        chasing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().Knockback(-1);
            Vector2 difference = new Vector2(transform.position.x, transform.position.y) - new Vector2(collision.transform.position.x, collision.transform.position.y);
            difference = difference.normalized * knockbackForce;
            rb.AddForce(difference, ForceMode2D.Impulse);
            if(attacking)player.GetComponent<Player>().TakeDamage(damage);
            
        }
    }
}
