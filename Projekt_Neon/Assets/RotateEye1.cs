using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEye1 : MonoBehaviour
{
    public float speed;
    public bool turnRight ;
    public int health;
    private string spriteNames = "HitSparksBlue";
    private GameObject hitSparks;
    private SpriteRenderer hitSparksR;
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        hitSparks = this.transform.Find("HitSparksBlau1").gameObject;
        hitSparksR= hitSparks.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnRight)RotateRight();
        else RotateLeft();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "AttackBox")
        {
           health--;
           Debug.Log("health: "+health);
           hitSparksR.sprite = sprites[Random.Range(0, 4)];
           hitSparksR.enabled = true;
           Invoke("showHitSparks",0.2f);
            
        }
 
        if (health == 0 )
        {
            var bossGetScript = GameObject.Find("Boss").GetComponent<BossScript>();
            bossGetScript.eyeCountDestroy++;
            GameObject.Find("Boss").GetComponent<Animator>().SetTrigger("GetHit");
            //GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            
        }
    }

     void RotateLeft () 
     {
        transform.Rotate(0, 0, Time.deltaTime * speed, Space.World);
     }

     void RotateRight () 
     {
        transform.Rotate(0, 0, Time.deltaTime * -speed, Space.World);
     }

         private void showHitSparks()
    {
        hitSparksR.enabled = false;
    }
}
