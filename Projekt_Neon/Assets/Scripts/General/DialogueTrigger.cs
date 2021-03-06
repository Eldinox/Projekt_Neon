﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if(this.name.Contains("Tor"))
        {
            if(player.GetComponent<Inventory>().collectedCoins > 4)
            {
                GameObject.Find("TorTrigger2").GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                GameObject.Find("TorTrigger1").GetComponent<BoxCollider2D>().enabled = true;
            }
        }  

        if(this.name.Contains("Lassie"))
        {
            if(player.GetComponent<Player>().sidequestActive == true)
            {
                if(player.GetComponent<Inventory>().collectedSticks >= 8 && player.GetComponent<Inventory>().collectedMushrooms >= 10 && player.GetComponent<Inventory>().collectedStones >= 3)
                {
                    GameObject.Find("oldLassie2").GetComponent<BoxCollider2D>().enabled = false;
                    GameObject.Find("oldLassie2").GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    GameObject.Find("oldLassie3").GetComponent<BoxCollider2D>().enabled = false;
                    GameObject.Find("oldLassie3").GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else
            {
                GameObject.Find("oldLassie2").GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("oldLassie2").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("oldLassie3").GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("oldLassie3").GetComponent<SpriteRenderer>().enabled = false;
            }
        } 
    }

    public void TriggerDialogue()
    {
        player.GetComponent<Player>().stopAnimation();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        if(this.name == "TorTrigger2")
        {
            GameObject.Find("Tor").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("OpeningDoor").GetComponent<Animator>().SetTrigger("Open");
        }
        else if(this.name == "oldLassie1")
        {
            GameObject.Find("Player").GetComponent<Player>().sidequestActive = true;
            GameObject.Find("Questfield3").GetComponent<TextMeshProUGUI>().text = "Nebenaufgabe: Kwokas berühmter Pilz-Eintopf";
            GameObject.Find("Questfield4").GetComponent<TextMeshProUGUI>().text = "Eingesammeltes Feuerholz " + GameObject.Find("Player").GetComponent<Inventory>().collectedSticks + " / 8";
            GameObject.Find("Questfield5").GetComponent<TextMeshProUGUI>().text = "Eingesammelte Pilze " + GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms + " / 10";
            GameObject.Find("Questfield6").GetComponent<TextMeshProUGUI>().text = "Eingesammelte Steine " + GameObject.Find("Player").GetComponent<Inventory>().collectedStones + " / 3";
        }
        else if(this.name == "oldLassie3")
        {
            GameObject.Find("Questfield3").GetComponent<TextMeshProUGUI>().text = "Nebenaufgabe: -";
            GameObject.Find("Questfield4").GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("Questfield5").GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("Questfield6").GetComponent<TextMeshProUGUI>().text = "";

            GameObject.Find("Player").GetComponent<Inventory>().collectedSticks -= 8;
            GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms -= 10;
            GameObject.Find("Player").GetComponent<Inventory>().collectedStones -= 3;

            GameObject.Find("Player").GetComponent<Inventory>().UpdateInventory();
            GameObject.Find("Player").GetComponent<Player>().sidequestActive = false;
        }
        else if(this.name == "tallBoi")
        {
            GameObject.Find("Coin").GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
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
            if(this.name == "EndGameTrigger")
            {
                GameObject.Find("DialogueBox").SetActive(false);
                GameObject.Find("GameEnd").GetComponent<Animator>().SetTrigger("gameEnd");
            }
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
            if(this.name == "oldLassie1" && player.GetComponent<Player>().sidequestActive == true)
            {
                if(Vector2.Distance(transform.position, player.transform.position) > 10)
                {
                    GameObject.Find("oldLassie1").SetActive(false);
                    if(player.GetComponent<Inventory>().collectedStones < 3)
                    {
                        GameObject.Find("oldLassie2").GetComponent<BoxCollider2D>().enabled = true;
                        GameObject.Find("oldLassie2").GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }
    }
}
