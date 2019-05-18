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
    public int form = 0; //0=normal, 1=strong, 2=ranged
    
    public Transform groundCheck;
    public Transform spawnPoint;
    public string respawnPoint;
    public float checkRadius;
    public LayerMask whatIsGround;
    public GameObject fireball;
    public GameObject powerwave;
    public GameObject shotPoint;
    public GameObject dagger;
    public GameObject daggerPos1;
    public GameObject daggerPos2;

    public GameObject bobNormalform;
    public GameObject bobRangeform;
    public GameObject bobDamageform;
    
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
    private bool dashed;
    private bool respawning;
    
    private Rigidbody2D rb;
    private Animator statusAnim;
    private bool[] coins;
    private GameObject gm;
    private Animator anim;
    private Animator BobNormalAnimator;
    private Animator BobRangeAnimator;
    private Animator BobStrongAnimator;
    private GameObject hitSparksBob;
    private string spriteNames = "HitSparksRed";
    public Sprite[] sprites;
    private SpriteRenderer hitSparksR;
    private GameObject deathMenuFirstButton;

    private GameObject[] bobNormalAllObjs;
    private GameObject[] bobStrongAllObjs;
    private GameObject[] bobRangeAllObjs;
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
        deathMenuFirstButton = GameObject.Find("RespawnButton");
        SceneManager.activeSceneChanged += ChangedActiveScene;
        anim = GetComponent<Animator>();
        BobNormalAnimator = bobNormalform.GetComponent<Animator>();
        BobRangeAnimator = bobRangeform.GetComponent<Animator>();
        BobStrongAnimator = bobDamageform.GetComponent<Animator>();
        gm = GameObject.Find("GameManager");
        hitSparksBob = this.transform.Find("HitSparksRedBob").gameObject;
        hitSparksR= hitSparksBob.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        bobNormalAllObjs = GameObject.FindGameObjectsWithTag("BobNormal");
        bobRangeAllObjs = GameObject.FindGameObjectsWithTag("BobRange");
        bobStrongAllObjs = GameObject.FindGameObjectsWithTag("BobStrong");
                    ActivateBobSprites(bobStrongAllObjs,false);
                    ActivateBobSprites(bobNormalAllObjs,true);
                    ActivateBobSprites(bobRangeAllObjs,false);
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if(!respawning)
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
            GameObject.Find("SideKick").transform.position = new Vector3(transform.position.x - 3, transform.position.y + 5, transform.position.z);
            GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);
        }
        else
        {
            transform.position = GameObject.Find("RespawnPoint").transform.position;
        }
        
        respawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inDialogue && !dead)
        {
            if(isGrounded == true)
            {
                extraJumps = jumpAmount;
                switch(form)
                {
                    case 0 : BobNormalAnimator.SetBool("isJumping", false);
                    break;
                    case 1 : BobStrongAnimator.SetBool("isJumping",false);
                    break;
                    case 2 : BobRangeAnimator.SetBool("isJumping", false);
                    break;
                }
            }
            else
            {
                switch(form)
                {
                    case 0 : BobNormalAnimator.SetBool("isJumping", true);
                    break;
                    case 1 : BobStrongAnimator.SetBool("isJumping",true);
                    break;
                    case 2 : BobRangeAnimator.SetBool("isJumping", true);
                    break;
                }
            }
            //Dolch ziehen wenn im Kampf, wegstecken nach Combat-Zeit
            if(Time.time >= combatTimer)
            {
                dagger.transform.position = daggerPos1.transform.position;
                dagger.transform.rotation = daggerPos1.transform.rotation;
                comboNumber = 0;
            }
            if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.JoystickButton2))
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
            if(Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.JoystickButton3))
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
            if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                if(form == 0)
                {
                    //Debug.Log("From normal to range");
                    form = 2;
                    ActivateBobSprites(bobStrongAllObjs,false);
                    ActivateBobSprites(bobRangeAllObjs,true); 
                    ActivateBobSprites(bobNormalAllObjs,false);
                    //var bobnormal  = this.transform.Find("Bob2").gameObject;
                    //bobnormal.GetComponent<IKManager2D>().enabled = false;
                    BobRangeAnimator.SetBool("isRunning", false);
                    speed = 19;
                    jumpForce = 23;
                }
                else if(form == 1)
                {
                    //Debug.Log("From damage to normal");
                    form = 0;              
                    ActivateBobSprites(bobStrongAllObjs,false);
                    ActivateBobSprites(bobNormalAllObjs,true);
                    ActivateBobSprites(bobRangeAllObjs,false);                                                
                    BobNormalAnimator.SetBool("isRunning", false);
                    speed = 17;
                    jumpForce = 21;
                }
                else if(form == 2)
                {
                    //Debug.Log("From range to damage");
                    form = 1;
                    //GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Strong";
                    ActivateBobSprites(bobNormalAllObjs,false);
                    ActivateBobSprites(bobRangeAllObjs,false); 
                    ActivateBobSprites(bobStrongAllObjs,true);
                    //dagger.SetActive(false);
                    BobStrongAnimator.SetBool("isRunning", false);
                    speed = 12;
                    jumpForce = 15;
                }
            }
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                if(form == 1)
                {
                    form = 2;
                    //GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Ranged";
                    ActivateBobSprites(bobNormalAllObjs,false);
                    ActivateBobSprites(bobRangeAllObjs,true);
                    ActivateBobSprites(bobStrongAllObjs,false);                   
                    BobRangeAnimator.SetBool("isRunning", false);
                    speed = 19;
                    jumpForce = 23;
                }
                else if(form == 2)
                {
                    form = 0;
                    //GameObject.Find("FormDisplay").GetComponent<TextMeshProUGUI>().text = "Normal";
                    ActivateBobSprites(bobNormalAllObjs,true);
                    ActivateBobSprites(bobRangeAllObjs,false);
                    ActivateBobSprites(bobStrongAllObjs,false); 
                    dagger.SetActive(true);
                    BobNormalAnimator.SetBool("isRunning", false);
                    speed = 17;
                    jumpForce = 21;
                }
                else if(form == 0)
                {
                    form = 1;
                    ActivateBobSprites(bobNormalAllObjs,false);
                    ActivateBobSprites(bobRangeAllObjs,false);
                    ActivateBobSprites(bobStrongAllObjs,true);
                    BobStrongAnimator.SetBool("isRunning", false);
                    speed = 12;
                    jumpForce = 15;
                }
            }
            if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                statusAnim = GameObject.Find("PlayerStatus").GetComponent<Animator>();;
                statusAnim.SetBool("isOpen", !statusAnim.GetBool("isOpen"));
            }
            if(Input.GetKeyDown(KeyCode.W) && extraJumps > 0 || Input.GetKeyDown(KeyCode.JoystickButton0) && extraJumps > 0)
            {
                switch(form)
                {
                    case 0 : BobNormalAnimator.SetTrigger("takeOf");
                    break;
                    case 1 : BobStrongAnimator.SetTrigger("takeOf");
                    break;
                    case 2 : BobRangeAnimator.SetTrigger("takeOf");
                    break;
                }
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            if(Input.GetKey(KeyCode.F) && Time.time >= fireballCooldown || Input.GetKeyDown(KeyCode.JoystickButton1) && Time.time >= fireballCooldown)
            {
                if(form == 0) //Heal
                {
                    StartCoroutine(Healing(1));
                }
                else if(form == 1)
                {
                    attackState = "Groundsmash";
                    BobStrongAnimator.SetTrigger("T2_SpecialAttack");
                    if(!isGrounded)StartCoroutine(Dash(0, -.7f));
                }
                else if(form == 2)
                {
                    BobRangeAnimator.SetTrigger("fireball");
                    Instantiate(fireball, shotPoint.transform.position, shotPoint.transform.rotation);
                }
                fireballCooldown = Time.time + fireballCooldownTime;
                GameObject.Find("FireballIconCD").GetComponent<CooldownDisplay>().StartCD(fireballCooldownTime);
            }
            if(Input.GetKey(KeyCode.W) && isJumping == true || Input.GetKey(KeyCode.JoystickButton0) && isJumping == true)
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
            if(Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.JoystickButton0))
            {
                isJumping = false;
            }

            if(Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Triggers") > 0)
            {
                if(dashTime > 0 && !dashed)
                {
                    dashTime -= Time.deltaTime;

                    if(facingRight)
                    {
                        StartCoroutine(Dash(1, .1f));
                    }
                    else
                    {
                        StartCoroutine(Dash(-1, .1f));
                    }
                    dashed = true;
                }
            }
            if(Input.GetKeyUp(KeyCode.Space) || Input.GetAxis("Triggers") == 0)
            {
                dashTime = startDashTime;
                dashed = false;
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
        
        if(!inDialogue && !dead)
        {
            if (moveInput == 0)
            {
                switch (form)
                {
                    case 0 : BobNormalAnimator.SetBool("isRunning",false);
                    break;
                    case 1 : BobStrongAnimator.SetBool("isRunning",false);
                    break;
                    case 2 : BobRangeAnimator.SetBool("isRunning",false);
                    break;
                }
               
            }
            else
            {
                switch (form)
                {
                    case 0 : BobNormalAnimator.SetBool("isRunning",true);
                    break;
                    case 1 : BobStrongAnimator.SetBool("isRunning",true);
                    break;
                    case 2 : BobRangeAnimator.SetBool("isRunning",true);
                    break;
                }
                
            }
            
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
    private void ActivateBobSprites(GameObject[] bobForm, bool status)
    {
        foreach (var obj in bobForm)
        {
            obj.GetComponent<SpriteRenderer>().enabled = status;
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
        rb.velocity = new Vector2(-10, .5f) * 20;
        
        var gmGetScript = gm.GetComponent<FeedbackDisplay>();
        if(form == 1) health -= damage / 2;
        else health -= damage;
        
        if(gmGetScript.getHitAnimation)
        {
             switch(form)
                {
                    case 0 : BobNormalAnimator.SetTrigger("getHitBob");
                    break;
                    case 1 : BobStrongAnimator.SetTrigger("getHitBob");
                    break;
                    case 2 : BobRangeAnimator.SetTrigger("getHitBob");
                    break;
                }
        }
        if(gmGetScript.getHitColoring)
        {
            switch(form)
                {
                    case 0 : BobNormalAnimator.SetTrigger("bobColorChange");
                    break;
                    case 1 : BobStrongAnimator.SetTrigger("bobColorChange");
                    break;
                    case 2 : BobRangeAnimator.SetTrigger("bobColorChange");
                    break;
                }

        }
        if(gmGetScript.hitSparks)
        {
            //Debug.Log ("lengthsprtiebob "+sprites.Length);
            hitSparksR.sprite = sprites[(int)Random.Range(0.0f, 5.0f)];
            hitSparksR.enabled = true;
            Invoke("showHitSparks",0.2f);
        }
        
        if(health < 1)
        {
            dead = true;
            //this.gameObject.SetActive(false);
            //GameObject.Find("DeathCanvas").SetActive(true);
            GameObject.Find("DeathScreen").GetComponent<Animator>().SetTrigger("death");
            
            //GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(deathMenuFirstButton, null);
        }
        else if(health > 100)
        {
            health = 100;
        }
        GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);
    }

    private void showHitSparks()
    {
        hitSparksR.enabled = false;
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
        SceneManager.LoadScene(respawnPoint);

        /*if(enteredLeft)transform.position = GameObject.Find("PlayerSpawnStart").transform.position;
        else if(!enteredLeft)transform.position = GameObject.Find("PlayerSpawnEnd").transform.position;*/
        respawning = true;
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

    private IEnumerator Healing(int heal)
    {
        for(int i = 0; i < 20; i++)
        {
            health += heal;
        
            if(health > 100)
            {
                health = 100;
            }
            GameObject.Find("HealthBar").GetComponent<HealthBar>().UpdateHealth(health);
            yield return new WaitForSeconds(.1f);
        }
        //yield return null;
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
    public IEnumerator Dash(float directionX, float directionY)
    {
        if(GameObject.Find("DashBar").GetComponent<DashBar>().CheckDash())
        {
            GameObject.Find("DashBar").GetComponent<DashBar>().UpdateDashbar();
            gameObject.layer = LayerMask.NameToLayer("PlayerDash");
            for(int i = 0; i < 15; i++)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + directionX, transform.position.y + directionY), dashSpeed);

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
                if(combo1 == 1 && combo2 == 1)
                {
                    attackState = "T1Light3";
                    BobNormalAnimator.SetTrigger("T1_LightAttack3");
                } 
                else if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "T1Big";
                    BobNormalAnimator.SetTrigger("T1_BigAttack3");
                } 
                else
                {
                    attackState = "T1Light";
                    BobNormalAnimator.SetTrigger("T1_LightAttack");
                } 
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 1;
                else if(comboNumber == 2) combo2 = 1;
                attackState = "T1Light";
                BobNormalAnimator.SetTrigger("T1_LightAttack");
            }
        }
        else if(form == 1)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "Powerwave";
                    Instantiate(powerwave, new Vector2(shotPoint.transform.position.x, shotPoint.transform.position.y - 1), shotPoint.transform.rotation);
                    BobStrongAnimator.SetTrigger("T2_PowerWave");
                } 
                else
                {
                    attackState = "T2Light";
                    BobStrongAnimator.SetTrigger("T2_LightAttack");
                } 
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 1;
                else if(comboNumber == 2) combo2 = 1;
                attackState = "T2Light";
                BobStrongAnimator.SetTrigger("T2_LightAttack");
            }
        }
        else if(form == 2)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "Groundslam";
                    BobRangeAnimator.SetTrigger("T3_Groundslam");
                } 
                else
                {
                    attackState = "T3Light";
                    BobRangeAnimator.SetTrigger("T3_LightAttack");
                } 
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 1;
                else if(comboNumber == 2) combo2 = 1;
                attackState = "T3Light";
                BobRangeAnimator.SetTrigger("T3_LightAttack");
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
                if(combo1 == 1 && combo2 == 1)
                {
                    attackState = "Weit";
                    BobNormalAnimator.SetTrigger("T1_HeavyAttack_weit");
                } 
                else 
                {
                    attackState = "T1Heavy";
                    BobNormalAnimator.SetTrigger("T1_HeavyAttack");
                }
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 2;
                else if(comboNumber == 2) combo2 = 2;
                attackState = "T1Heavy";
                BobNormalAnimator.SetTrigger("T1_HeavyAttack");
            }
        }
        else if(form == 1)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "T2Heavy3";
                    BobStrongAnimator.SetTrigger("T2_HeavyAttack3");
                } 
                else if(combo1 == 1 && combo2 == 1)
                {
                    attackState = "Overhead";
                    BobStrongAnimator.SetTrigger("T2_OverheadAttack");
                } 
                else
                {
                    attackState = "T2Heavy";
                    BobStrongAnimator.SetTrigger("T2_HeavyAttack");
                } 
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 2;
                else if(comboNumber == 2) combo2 = 2;
                attackState = "T2Heavy";
                BobStrongAnimator.SetTrigger("T2_HeavyAttack");
            }
        }
        else if(form == 2)
        {
            if(comboNumber == 3)
            {
                if(combo1 == 2 && combo2 == 2)
                {
                    attackState = "T3Heavy3";
                    BobRangeAnimator.SetTrigger("T3_HeavyAttack3");
                } 
                else if(combo1 == 1 && combo2 == 1)
                {
                    attackState = "Uppercut";
                    BobRangeAnimator.SetTrigger("T3_Uppercut");
                } 
                else
                {
                    attackState = "T3Heavy";
                    BobRangeAnimator.SetTrigger("T3_HeavyAttack");
                } 
                combo1 = 0;
                combo2 = 0;
            }
            else
            {
                if(comboNumber == 1) combo1 = 2;
                else if(comboNumber == 2) combo2 = 2;
                attackState = "T3Heavy";
                BobRangeAnimator.SetTrigger("T3_HeavyAttack");
            }
        }
        
        Debug.Log(comboNumber);
        yield return null;
    }
}
