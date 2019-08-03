using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool onlyOnce;
    public bool automatic = false;
    public GameObject abutton;

    [HideInInspector]
    public GameObject player;

    private bool done;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        done = true;
    }

    private void WaitForTrigger()
    {
        abutton.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Player>().dialoguePossible = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(onlyOnce == false || done == false)
            {
                if(automatic)TriggerDialogue();
                else WaitForTrigger();
            }
        }
    }

    void Update()
    {
        if(automatic == false)
        {
            if(abutton.GetComponent<SpriteRenderer>().enabled == true)
            {
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    TriggerDialogue();
                    abutton.GetComponent<SpriteRenderer>().enabled = false;
                    player.GetComponent<Player>().dialoguePossible = false;
                }
                if(Vector2.Distance(transform.position, player.transform.position) > 10)
                {
                    abutton.GetComponent<SpriteRenderer>().enabled = false;
                    player.GetComponent<Player>().dialoguePossible = false;
                }
            }
        }
    }
}
