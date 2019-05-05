using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float stopDistance;
    public float knockbackForce;
    public Transform[] patrolSpots;
    public float startWaitTime;
    public int jumpersInScene;

    private float attackTime;
    private Rigidbody2D rb;
    private int direction;
    private int randomSpot;
    private float waitTime;

    public Enemy jumper;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        randomSpot = Random.Range(0, patrolSpots.Length);
        waitTime = startWaitTime;

        /*GameObject[] allObjs = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        List<GameObject> jumpers = new List<GameObject>();
        foreach(GameObject obj in allObjs)
        {
            if(obj.name.Contains("Jumper"))
            {
                jumpers.Add(obj);
            }
        }
        jumpersInScene = (jumpers.ToArray()).Length;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && Vector2.Distance(transform.position, player.position) < activateDistance)
        {
            if(facingRight == false && attacking == false && player.transform.position.x > transform.position.x)
            {
                Flip();
            }
            else if(facingRight == true && attacking == false && player.transform.position.x < transform.position.x)
            {
                Flip();
            }
            
            //if(facingRight)direction = -1;
            //else direction = 1;
            //RaycastHit2D vision = Physics2D.Raycast(transform.position, transform.right * direction, spottingRange);
            
            if(Vector2.Distance(transform.position, player.position) < stopDistance)state = "attacking";
            //else if(vision.collider != null && vision.collider.CompareTag("Player"))state = "chasing";
            else if(Vector2.Distance(transform.position, player.position) < spottingRange)state = "chasing";
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
                //Debug.DrawLine(transform.position, transform.position + transform.right * spottingRange, Color.green);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                //Debug.DrawLine(transform.position, transform.position + transform.right * spottingRange, Color.green);
                transform.position = Vector2.MoveTowards(transform.position, patrolSpots[randomSpot].position, speed/4 * Time.deltaTime);
                if(patrolSpots[randomSpot].position.x > transform.position.x && facingRight == false)Flip();
                else if(patrolSpots[randomSpot].position.x < transform.position.x && facingRight == true)Flip();

                if(Vector2.Distance(transform.position, patrolSpots[randomSpot].position) < 5)
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
                }
            }
        }
    }

    IEnumerator Attack()
    {
        int jumpersSpawned;
        GameObject[] allObjs = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> jumpers = new List<GameObject>();
        foreach(GameObject obj in allObjs)
        {
            if(obj.name == "Jumper(Clone)")
            {
                jumpers.Add(obj);
            }
        }
        jumpersSpawned = (jumpers.ToArray()).Length;
        
        if(jumpersSpawned < 6)
        {
            attacking = true;
            int attackDirection = 0;
            if(player.transform.position.x < transform.position.x)attackDirection = -6;
            else attackDirection = 6;
            yield return new WaitForSeconds(1);
            Instantiate(jumper, new Vector2(transform.position.x + attackDirection, transform.position.y), transform.rotation);
            yield return new WaitForSeconds(.5f);
            attacking = false;
        }
    }
    IEnumerator Chase()
    {
        chasing = true;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        yield return null;
        chasing = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 difference = new Vector2(transform.position.x, transform.position.y) - new Vector2(collision.transform.position.x, collision.transform.position.y);
            difference = difference.normalized * knockbackForce;
            rb.AddForce(difference, ForceMode2D.Impulse);
            
            player.GetComponent<Player>().TakeDamage(damage);
            //player.GetComponent<Player>().Knockback(transform.position);
        }
    }
}
