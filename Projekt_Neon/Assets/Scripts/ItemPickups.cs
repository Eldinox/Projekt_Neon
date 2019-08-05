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
                        if(GameObject.Find("Player").GetComponent<Player>().sidequestActive == true)
                        {
                            GameObject.Find("Questfield4").GetComponent<TextMeshProUGUI>().text = "Eingesammeltes Feuerholz " + GameObject.Find("Player").GetComponent<Inventory>().collectedSticks + " / 8";
                        }
                    }
                    else if(gameObject.name == "Stone")
                    {
                        Instantiate(drop, inventory.slots[i].transform, false);
                        GameObject.Find("Player").GetComponent<Inventory>().collectedStones++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedStones.ToString();
                        GameObject.Find("StoneIcon(Clone)").transform.position = inventory.slots[i].transform.position;
                        if(GameObject.Find("Player").GetComponent<Player>().sidequestActive == true)
                        {
                            GameObject.Find("Questfield6").GetComponent<TextMeshProUGUI>().text = "Eingesammelte Steine " + GameObject.Find("Player").GetComponent<Inventory>().collectedStones + " / 3";
                        }
                    }
                    else if(gameObject.name == "Mushroom")
                    {
                        Instantiate(drop, inventory.slots[i].transform, false);
                        GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms.ToString();
                        GameObject.Find("MushroomIcon(Clone)").transform.position = inventory.slots[i].transform.position;
                        if(GameObject.Find("Player").GetComponent<Player>().sidequestActive == true)
                        {
                            GameObject.Find("Questfield5").GetComponent<TextMeshProUGUI>().text = "Eingesammelte Pilze " + GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms + " / 10";
                        }
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
                        if(GameObject.Find("Player").GetComponent<Player>().sidequestActive == true)
                        {
                            GameObject.Find("Questfield4").GetComponent<TextMeshProUGUI>().text = "Eingesammeltes Feuerholz " + GameObject.Find("Player").GetComponent<Inventory>().collectedSticks + " / 8";
                        }
                    }
                    else if(gameObject.name == "Stone")
                    {
                        GameObject.Find("Player").GetComponent<Inventory>().collectedStones++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedStones.ToString();
                        if(GameObject.Find("Player").GetComponent<Player>().sidequestActive == true)
                        {
                            GameObject.Find("Questfield6").GetComponent<TextMeshProUGUI>().text = "Eingesammelte Steine " + GameObject.Find("Player").GetComponent<Inventory>().collectedStones + " / 3";
                        }
                    }
                    else if(gameObject.name == "Mushroom")
                    {
                        GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms++;
                        GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms.ToString();
                        if(GameObject.Find("Player").GetComponent<Player>().sidequestActive == true)
                        {
                            GameObject.Find("Questfield5").GetComponent<TextMeshProUGUI>().text = "Eingesammelte Pilze " + GameObject.Find("Player").GetComponent<Inventory>().collectedMushrooms + " / 10";
                        }
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
