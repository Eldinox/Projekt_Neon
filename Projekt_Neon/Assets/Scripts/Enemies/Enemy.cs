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
    public GameObject enemyCanvas;
    public Image healthBarImage;
    
    public AudioClip EnemyGetHitSound;
    private AudioSource EnemyAudioSource;
    
    [HideInInspector]
    public Transform player;
    //[HideInInspector]
    public string state;

    [Header("Gegner Bools")]
    public bool facingRight = false;
    public bool attacking = false;
    public bool chasing = false;
    public bool stunned = false;
    public bool dead = false;

    private GameObject gm;
    private GameObject hitSparks;
    private Animator anim;
    private SpriteRenderer hitSparksR;
    private float health;
    private string spriteNames = "HitSparksBlue";
    private Sprite[] sprites;

    private Sprite stunSprite;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gm = GameObject.Find("GameManager");
        health = startHealth;
        anim = GetComponent<Animator>();
        EnemyAudioSource = gm.GetComponent<AudioSource>();
        
   
        hitSparks = this.transform.Find("HitSparksBlau1").gameObject;
        hitSparksR= hitSparks.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        stunSprite = Resources.Load<Sprite>("HitSparks_boob3");
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector2.Distance(transform.position, player.position);
    }

    public void TakeDamage(float damageAmount)
    {
        if(gameObject.name.Contains("Spider"))
        {
            Stun(1);
        }
        
        var gmGetScript = gm.GetComponent<FeedbackDisplay>();
    	health -= damageAmount;
        if(gmGetScript.healthBars)
        {
            healthBarImage.fillAmount = health / startHealth;
        }
        if(gmGetScript.getHitAnimation)
        {
            anim.SetTrigger("getHit");
        }
        if(gmGetScript.getHitColoring)
        {
            anim.SetTrigger("changeColor");
        }
        if(gmGetScript.hitSparks)
        {
           Debug.Log ("lengthsprtie"+sprites.Length);
           Debug.Log("RandomRange"+Random.Range(0, 4));
           hitSparksR.sprite = sprites[Random.Range(0, 4)];
           hitSparksR.enabled = true;
           Invoke("showHitSparks",0.2f);

        }

        EnemyGetHitSound = Resources.Load<AudioClip>("Sounds/GetHitSoundEffect");
        EnemyAudioSource.clip = EnemyGetHitSound;
        EnemyAudioSource.Play(0);
        
        if(gmGetScript.damageNumberDisplay)enemyCanvas.GetComponentInChildren<TextMeshProUGUI>().text = damageAmount.ToString();
        Invoke("Displaytime", 0.3f);

    	if(health <= 0)
    	{
    		gameObject.layer = LayerMask.NameToLayer("EnemyDead");
            dead = true;
            Invoke("Delete", 3);
    	}
    }

    public void KnockDown(float duration)
    {
        stunned = true;
        //Hier code für visuellen knockdown
        var gmGetScript = gm.GetComponent<FeedbackDisplay>();
        if(gmGetScript.damageNumberDisplay)enemyCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "down";
        Invoke("Displaytime", duration);
        Invoke("Stuntime", duration);

    }

    public void Stun(float duration)
    {
        stunned = true;
        hitSparksR.sprite = stunSprite;
        hitSparksR.enabled =true;
        //Hier code für visuellen stun
        var gmGetScript = gm.GetComponent<FeedbackDisplay>();
        if(gmGetScript.damageNumberDisplay)enemyCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "@@@";
        Invoke("Displaytime", duration);
        Invoke("Stuntime", duration);
        
    }

    private void Stuntime()
    {
        stunned = false;
        hitSparksR.enabled =false;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        if(facingRight)enemyCanvas.transform.rotation = Quaternion.Euler(0, 180, 0);
        else enemyCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Displaytime()
    {
        var gmGetScript = gm.GetComponent<FeedbackDisplay>();
        if(gmGetScript.damageNumberDisplay)enemyCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
    private void showHitSparks()
    {
        hitSparksR.enabled = false;
    }
    private void Delete()
    {
        Destroy(this.gameObject);
    }
}
