using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerwaveBox : MonoBehaviour
{
    private GameObject powerwave;
    
    // Start is called before the first frame update
    void Start()
    {
        powerwave = GameObject.Find("Powerwave(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if(powerwave != null)
        {
            transform.position = powerwave.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Stun(2);
        }
    }
}
