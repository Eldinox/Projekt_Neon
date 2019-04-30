using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SideKickFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform PlayerPosition;
    private Rigidbody2D SideKickPosition;
    private GameObject Player;

    public float speed;
    public float X ;
    public float Y ;
    public float distanceToPlayer;
    private bool test1;
    private bool facingRight = false;
    private SpriteRenderer sideKickSPR;



    void Start()
    {
    
        Player = GameObject.Find("Player");
        sideKickSPR = this.transform.Find("Sidekick_2").gameObject.GetComponent<SpriteRenderer>();
        

    }

    // Debug.Log("facingRight"+Update is called once per frame
    void FixedUpdate()
    {  
        var getScriptPlayer = Player.GetComponent<Player>();
        facingRight = getScriptPlayer.facingRight; 

        if (Vector2.Distance(transform.position, Player.transform.position)> distanceToPlayer)
        {
            
            //anim.enabled = !anim.enabled;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position+ new Vector3(X,Y, 0), speed * Time.deltaTime);
           // Debug.Log(Player.transform.position);
        }

         if(facingRight == false )
            {
                sideKickSPR.flipX = true;
            }
            else if(facingRight == true)
            {
                sideKickSPR.flipX = false;
            }



    }



}
