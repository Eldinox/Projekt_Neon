using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    //Werte die sich im Spiel ändern
    public int health;
    
    //Feste Werte
    public float speed;
    public float jumpForce;
    public int jumpAmount;
    public float jumpTime;
    public float fireballCooldownTime;
    public float knockbackForce;
    public float knockbackDuration;
    public float dashSpeed;
    public float startDashTime;
    public float combatTime;
    public bool dead;
    public bool inDialogue;
    public int coinAmount;
    public float timeBetweenCombos;
    public string attackState;
    public bool aircombat;
    public bool isGrounded;
    
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform spawnPoint;
    public GameObject fireball;
    public GameObject shotPoint;
    public GameObject dagger;
    public GameObject daggerPos1;
    public GameObject daggerPos2;

    public GameObject bobNormalform;
    
    private float moveInput;
    public  bool facingRight = true;
    private bool isJumping;
    private bool enteredLeft = true;
    private int extraJumps;
    private float jumpTimeCounter;
    private float dashTime;
    private float fireballCooldown;
    private float combatTimer;
    private int comboNumber;
    private float comboTime;
    private int combo1;
    private int combo2;
    private int form; //0=normal, 1=strong, 2=ranged
    
    private Rigidbody2D rb;
    private Animator statusAnim;
    private bool[] coins;
    private GameObject gm;
    private Animator anim;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        coins = new bool[coinAmount];
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = jumpAmount;
        dashTime = startDashTime;
        SceneManager.activeSceneChanged += ChangedActiveScene;
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager");
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
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isJumping", true);
            }
            //Dolch ziehen wenn im Kampf, wegstecken nach Combat-Zeit
            if(Time.time >= combatTimer)
            {
                dagger.transform.position = daggerPos1.transform.position;
                dagger.transform.rotation = daggerPos1.transform.rotation;
                comboNumber = 0;
            }
            if(Input.GetMouseButton(0))
            {
                dagger.transform.position = daggerPos2.transform.position;
                dagger.transform.rotation = daggerPos2.transform.rotation;
                combatTimer = Time.time + combatTime;

                if(Time.time > comboTime)
                {
                    if(facingRight)
                    {
                        StartCoroutine(LightAttack(1));
                    }
                    else
                    {
                        StartCoroutine(LightAttack(-1));
                    }
                }
            }
            if(Input.GetMouseButton(1))
            {
                dagger.transform.position = daggerPos2.transform.position;
                dagger.transform.rotation = daggerPos2.transform.rotation;
                combatTimer = Time.time + combatTime;

                if(Time.time > comboTime)
                {
                    if(facingRight)
                    {
                        StartCoroutine(HeavyAttack(1));
                    }
                    else
                    {
                        StartCoroutine(HeavyAttack(-1));
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if(form == 0)
                {
                    form = 2;
                    GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Ranged";
                    GameObject.Find("Bob_Ranger").GetComponent<SpriteRenderer>().enabled = true;
                    bobNormalform.SetActive(false);
                }
                else if(form == 1)
                {
                    form = 0;
                    GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Normal";
                    GameObject.Find("Bob_Damage").GetComponent<SpriteRenderer>().enabled = false;
                    bobNormalform.SetActive(true);
                }
                else if(form == 2)
                {
                    form = 1;
                    GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Strong";
                    GameObject.Find("Bob_Ranger").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("Bob_Damage").GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(form == 1)
                {
                    form = 2;
                    GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Ranged";
                    GameObject.Find("Bob_Ranger").GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("Bob_Damage").GetComponent<SpriteRenderer>().enabled = false;
                }
                else if(form == 2)
                {
                    form = 0;
                    GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Normal";
                    GameObject.Find("Bob_Ranger").GetComponent<SpriteRenderer>().enabled = false;
                    bobNormalform.SetActive(true);
                }
                else if(form == 0)
                {
                    form = 1;
                    GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Strong";
                    GameObject.Find("Bob_Damage").GetComponent<SpriteRenderer>().enabled = true;
                    bobNormalform.SetActive(false);
                }
            }
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                statusAnim = GameObject.Find("PlayerStatus").GetComponent<Animator>();;
                statusAnim.SetBool("isOpen", !statusAnim.GetBool("isOpen"));
            }
            if(Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
            {
                anim.SetTrigger("takeOf");
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            if(Input.GetKey(KeyCode.F) && Time.time >= fireballCooldown)
            {
                if(form == 0) //Heal
                {
                    TakeDamage(-10);
                }
                else if(form == 1)
                {
                    anim.SetTrigger("T2_SpecialAttack");
                }
                else if(form == 2)
                {
                    Instantiate(fireball, shotPoint.transform.position, shotPoint.transform.rotation);
                }
                fireballCooldown = Time.time + fireballCooldownTime;
                GameObject.Find("FireballIconCD").GetComponent<CooldownDisplay>().StartCD(fireballCooldownTime);
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
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
    	moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            anim.SetBool("isRunning",false);
        }
        else
        {
            anim.SetBool("isRunning",true);
        }
        if(!inDialogue)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
            //Input.GetAxisRaw("Horizontal"); <- damit Player sofort anhält (kein sliden)
            
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
        var gmGetScript = gm.GetComponent<FeedbackDisplay>();
        health -= damage;
        if(gmGetScript.getHitAnimation)
        {
            anim.SetTrigger("getHitBob");
        }
        if(gmGetScript.getHitColoring)
        {
            anim.SetTrigger("bobColorChange");

        }
        
        if(health < 1)
        {
            dead = true;
            this.gameObject.SetActive(false);
            GameObject.Find("DeathScreen").GetComponent<Animator>().SetTrigger("death");
        }
        else if(health > 100)
        {
            health = 100;
        }
        GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);
    }

    public void UpdateCoins(int number)
    {
        coins[number] = true;
        /*for(int i = 0; i < coins.Length; i++)
        {
            Debug.Log(coins[i]);
        }*/
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
        if(GameObject.Find("DashBar").GetComponent<DashBar>().CheckDash())
        {
            GameObject.Find("DashBar").GetComponent<DashBar>().UpdateDashbar();
            gameObject.layer = LayerMask.NameToLayer("PlayerDash");
            for(int i = 0; i < 10; i++)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + direction, transform.position.y + 0.1f), dashSpeed);

                yield return null;
            }
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    public IEnumerator LightAttack(int direction)
    {
        comboTime = Time.time + timeBetweenCombos;
        if(comboNumber < 3)comboNumber++;
        else comboNumber = 1;

        /* for(int i = 0; i < 3; i++)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + direction, transform.position.y), dashSpeed);
        }*/

        Animator anim = this.GetComponent<Animator>();
        attackState = "";
        if(form == 0)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 1 && combo2 == 1) anim.SetTrigger("T1_LightAttack3");
                else if(combo1 == 2 && combo2 == 2) anim.SetTrigger("T1_BigAttack3");
                else anim.SetTrigger("T1_LightAttack");
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 1;
                else if(comboNumber == 2) combo2 = 1;
                anim.SetTrigger("T1_LightAttack");
            }
        }
        else if(form == 1)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "Powerwave";
                    anim.SetTrigger("T2_PowerWave");
                } 
                else anim.SetTrigger("T2_LightAttack");
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 1;
                else if(comboNumber == 2) combo2 = 1;
                anim.SetTrigger("T2_LightAttack");
            }
        }
        else if(form == 2)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "Groundslam";
                    anim.SetTrigger("T3_Groundslam");
                } 
                else anim.SetTrigger("T3_LightAttack");
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 1;
                else if(comboNumber == 2) combo2 = 1;
                anim.SetTrigger("T3_LightAttack");
            }
        }
        
        Debug.Log(comboNumber);
        yield return null;
    }
    public IEnumerator HeavyAttack(int direction)
    {
        comboTime = Time.time + timeBetweenCombos;
        if(comboNumber < 3)comboNumber++;
        else comboNumber = 1;

        Animator anim = this.GetComponent<Animator>();
        attackState = "";
        if(form == 0)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 1 && combo2 == 1) anim.SetTrigger("T1_HeavyAttack_weit");
                else anim.SetTrigger("T1_HeavyAttack");
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 2;
                else if(comboNumber == 2) combo2 = 2;
                anim.SetTrigger("T1_HeavyAttack");
            }
        }
        else if(form == 1)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2) anim.SetTrigger("T2_HeavyAttack3");
                else if(combo1 == 1 && combo2 == 1) anim.SetTrigger("T2_OverheadAttack");
                else anim.SetTrigger("T2_HeavyAttack");
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 2;
                else if(comboNumber == 2) combo2 = 2;
                anim.SetTrigger("T2_HeavyAttack");
            }
        }
        else if(form == 2)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2) anim.SetTrigger("T3_HeavyAttack3");
                else if(combo1 == 1 && combo2 == 1)
                {
                    anim.SetTrigger("T3_Uppercut");
                    attackState = "Uppercut";
                } 
                else anim.SetTrigger("T3_HeavyAttack");
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 2;
                else if(comboNumber == 2) combo2 = 2;
                anim.SetTrigger("T3_HeavyAttack");
                attackState = "T3Attack";
            }
        }
        
        Debug.Log(comboNumber);
        yield return null;
    }
}
