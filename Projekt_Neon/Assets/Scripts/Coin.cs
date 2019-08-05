using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour
{
    public int number;
    private Inventory inventory;
    public GameObject coinIcon;
    public AudioClip coinSound;
    private AudioSource CoinAudioSource;
    //private TextMeshProUGUI coincount;

    void Start()
    {
        //coincount = GetComponent<TextMeshProUGUI>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(GameObject.Find("Player").GetComponent<Player>().coins[number] == true)
        {
            gameObject.SetActive(false);
        }
        CoinAudioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        CoinAudioSource.clip = coinSound;
        CoinAudioSource.Play(0);
    	if(collision.CompareTag("Player"))
    	{
           
    		collision.GetComponent<Player>().UpdateCoins(number);
            int coins = GameObject.Find("Player").GetComponent<Inventory>().collectedCoins + 1;
            GameObject.Find("Questfield2").GetComponent<TextMeshProUGUI>().text = "Schlüssel gefunden: " + coins + " / 5";

            for(int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    GameObject.Find("Player").GetComponent<Inventory>().inventoryItems[i] = gameObject.name;
                    Instantiate(coinIcon, inventory.slots[i].transform, false);
                    GameObject.Find("Player").GetComponent<Inventory>().collectedCoins++;
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedCoins.ToString();
                    GameObject.Find("CoinIcon(Clone)").transform.position = inventory.slots[i].transform.position;
                    inventory.isFull[i] = true;
                    break;
                }
                else if(GameObject.Find("Player").GetComponent<Inventory>().inventoryItems[i] == gameObject.name)
                {
                    GameObject.Find("Player").GetComponent<Inventory>().collectedCoins++;
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedCoins.ToString();
                    break;
                }
            }


            //int prevAmount = int.Parse(coincount.text);
            //int newAmount = prevAmount + 1;
            //coincount.text = "hi";//newAmount.ToString();
    		GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled =false;
            Invoke("DestroyCoin",1);
    	}
    }

    private void DestroyCoin()
    {
        Destroy(gameObject);
    }


}
