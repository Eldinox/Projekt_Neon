using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Werte die sich im Spiel ändern
    public int health;
    
    //Feste Werte
    public float speed;
    public float jumpForce;
    public int jumpAmount;
    public float jumpTime;
    public float knockbackForce;
    public float knockbackDuration;
    public float dashSpeed;
    public float startDashTime;
    public bool dead;
    public bool inDialogue;
    
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform spawnPoint;
    
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isJumping;
    private bool enteredLeft = true;
    private int extraJumps;
    private float jumpTimeCounter;
    private float dashTime;

    private Rigidbody2D rb;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = jumpAmount;
        dashTime = startDashTime;
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        spawnPoint = GameObject.Find("PlayerSpawnStart").transform;

        if(transform.position.x < spawnPoint.position.x)
        {
            spawnPoint = GameObject.Find("PlayerSpawnEnd").transform;
            transform.position = spawnPoint.position;
            enteredLeft = false;
        }
        else if(transform.position.x > spawnPoint.position.x)
        {
            transform.position = spawnPoint.position;
            enteredLeft = true;
        }

        GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inDialogue)
        {
            if(isGrounded == true)
            {
                extraJumps = jumpAmount;
            }

            if(Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            if(Input.GetKey(KeyCode.W) && isJumping == true)
            {
                if(jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                isJumping = false;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(dashTime > 0)
                {
                    dashTime -= Time.deltaTime;

                    if(facingRight)
                    {
                        StartCoroutine(Dash(1));
                    }
                    else
                    {
                        StartCoroutine(Dash(-1));
                    }
                }
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                dashTime = startDashTime;
            }
        }
    }

    private void FixedUpdate()
    {
    	if(!inDialogue)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
            //Input.GetAxisRaw("Horizontal"); <- damit Player sofort anhält (kein sliden)
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if(facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if(facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);

        if(health < 1)
        {
            dead = true;
            this.gameObject.SetActive(false);
            GameObject.Find("DeathScreen").GetComponent<Animator>().SetTrigger("death");
        }
    }

    public void RespawnAfterDeath()
    {
        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(activeScene.name);

        if(enteredLeft)transform.position = GameObject.Find("PlayerSpawnStart").transform.position;
        else if(!enteredLeft)transform.position = GameObject.Find("PlayerSpawnEnd").transform.position;
        health = 100;
        dead = false;
        GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);
        GameObject.Find("DeathScreen").GetComponent<Animator>().SetBool("death", false);
        this.gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //StartCoroutine(Knockback(collision.transform));
        }
    }

    public IEnumerator Knockback(Transform direction)
    {
        float timer = 0;
        
        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector2(direction.position.x * knockbackForce * 100, direction.position.y * knockbackForce), ForceMode2D.Impulse);
            Flip();
        }
        yield return 0;
    }
    public IEnumerator Dash(int direction)
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        for(int i = 0; i < 10; i++)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + direction, transform.position.y), dashSpeed);

            yield return null;
        }
        GetComponent<PolygonCollider2D>().enabled = true;
    }
}
