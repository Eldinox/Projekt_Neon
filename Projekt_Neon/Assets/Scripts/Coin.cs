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
    //private TextMeshProUGUI coincount;

    void Start()
    {
        //coincount = GetComponent<TextMeshProUGUI>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.CompareTag("Player"))
    	{
    		collision.GetComponent<Player>().UpdateCoins(number);

            for(int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    GameObject.Find("Player").GetComponent<Player>().inventoryItems[i] = gameObject.name;
                    Instantiate(coinIcon, inventory.slots[i].transform, false);
                    GameObject.Find("Player").GetComponent<Player>().collectedCoins++;
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Player>().collectedCoins.ToString();
                    GameObject.Find("CoinIcon(Clone)").transform.position = inventory.slots[i].transform.position;
                    inventory.isFull[i] = true;
                    break;
                }
                else if(GameObject.Find("Player").GetComponent<Player>().inventoryItems[i] == gameObject.name)
                {
                    GameObject.Find("Player").GetComponent<Player>().collectedCoins++;
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Player>().collectedCoins.ToString();
                    break;
                }
            }


            //int prevAmount = int.Parse(coincount.text);
            //int newAmount = prevAmount + 1;
            //coincount.text = "hi";//newAmount.ToString();
    		Destroy(gameObject);
    	}
    }
}
