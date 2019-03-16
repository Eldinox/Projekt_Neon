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
    public float speed = 2;
    public float distanceToPlayer;
    private bool test1;
 



    void Start()
    {
    
        Player = GameObject.Find("Player");
 


    }

    // Update is called once per frame
    void FixedUpdate()
    {   


        if (Vector2.Distance(transform.position, Player.transform.position)> distanceToPlayer)
        {
            //anim.enabled = !anim.enabled;
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position+ new Vector3(0,3, 0), speed * Time.deltaTime);
           // Debug.Log(Player.transform.position);
        }



    }


}
