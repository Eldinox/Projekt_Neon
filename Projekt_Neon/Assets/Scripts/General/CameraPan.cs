using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public GameObject destination;
    public float speed;
    public float endTime;
    public float zoom;
    
    private bool moving;
    
    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().playerTarget = this.gameObject.transform;
            GameObject.Find("Player").GetComponent<Player>().inDialogue = true;
            moving = true;
            Invoke("Disable", endTime);
        }
    }

    void Disable()
    {
        moving = false;
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().playerTarget = GameObject.Find("Player").transform;
        GameObject.Find("Player").GetComponent<Player>().inDialogue = false;
        this.gameObject.SetActive(false);
    }
}
