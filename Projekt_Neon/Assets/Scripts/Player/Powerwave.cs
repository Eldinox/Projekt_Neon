using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerwave : MonoBehaviour
{
    public float speed;
    public float duration;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", duration);
        rb = GetComponent<Rigidbody2D>();
        if(GameObject.Find("Player").GetComponent<Player>().facingRight == false)speed = speed * -1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
