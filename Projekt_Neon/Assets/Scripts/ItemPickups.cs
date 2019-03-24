using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickups : MonoBehaviour
{
    private Inventory inventory;
    public GameObject drop;
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
    		for(int i = 0; i < inventory.slots.Length; i++)
            {
                
                
                if(inventory.isFull[i] == false)
                {
                    GameObject.Find("Player").GetComponent<Inventory>().inventoryItems[i] = gameObject.name;
                    if(gameObject.name == "Stick")
                    {
                        Instantiate(drop, inventory.slots[i].transform, false);
                        GameObject.Find("Player").GetComponent<Inventory>().collectedSticks++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedSticks.ToString();
                        GameObject.Find("StickIcon(Clone)").transform.position = inventory.slots[i].transform.position;
                    }
                    else if(gameObject.name == "Stone")
                    {
                        Instantiate(drop, inventory.slots[i].transform, false);
                        GameObject.Find("Player").GetComponent<Inventory>().collectedStones++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedStones.ToString();
                        GameObject.Find("StoneIcon(Clone)").transform.position = inventory.slots[i].transform.position;
                    }
                    inventory.isFull[i] = true;
                    break;
                }
                else if(GameObject.Find("Player").GetComponent<Inventory>().inventoryItems[i] == gameObject.name)
                {
                    if(gameObject.name == "Stick")
                    {
                        GameObject.Find("Player").GetComponent<Inventory>().collectedSticks++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedSticks.ToString();
                    }
                    else if(gameObject.name == "Stone")
                    {
                        GameObject.Find("Player").GetComponent<Inventory>().collectedStones++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedStones.ToString();
                    }
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
