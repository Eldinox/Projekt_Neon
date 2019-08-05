using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public float stopDistance;
    public GameObject projectile;
    public Transform shotPoint;

    public AudioClip SpiderSound;
    private AudioSource SpiderAudioSource;

    private float attackTime;

    // Start is called before the first frame update
    public override void Start()
    {
    	base.Start();
        SpiderAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Player>().dead == false && !stunned && !dead)
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
            if(facingRight == false && player.transform.position.x > transform.position.x)
            {
                Flip();
            }
            else if(facingRight == true && player.transform.position.x < transform.position.x)
            {
                Flip();
            }
        }

        if (dead)
        {
            GameObject.Find("Spider").GetComponent<Animator>().SetTrigger("isDead");
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(.5f);
        if(!stunned)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            SpiderAudioSource.clip = SpiderSound;
            SpiderAudioSource.Play(0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           
        }
    }
}
