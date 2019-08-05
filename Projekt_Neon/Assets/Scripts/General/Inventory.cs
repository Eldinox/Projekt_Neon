using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    public int collectedCoins;
    public int collectedSticks;
    public int collectedStones;
    public int collectedMushrooms;
    public string[] inventoryItems;

    public GameObject coinIcon, stickIcon, stoneIcon, mushroomIcon;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        for(int i = 0; i < slots.Length; i++)
        {
            string name = "InventorySlot" + i;
            slots[i] = GameObject.Find("InventorySlot" + i.ToString());
        }

        inventoryItems = new string[6];
        collectedCoins = 0;
        collectedSticks = 0;
        collectedStones = 0;
        collectedMushrooms = 0;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            string name = "InventorySlot" + i;
            slots[i] = GameObject.Find(name);

            if(isFull[i] == true)
            {
                if(inventoryItems[i] == "Coin")
                {
                    Instantiate(coinIcon, slots[i].transform, false);
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = collectedCoins.ToString();
                    GameObject.Find("CoinIcon(Clone)").transform.position = slots[i].transform.position;
                }
                else if(inventoryItems[i] == "Stick")
                {
                    Instantiate(stickIcon, slots[i].transform, false);
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = collectedSticks.ToString();
                    GameObject.Find("StickIcon(Clone)").transform.position = slots[i].transform.position;
                }
                else if(inventoryItems[i] == "Stone")
                {
                    Instantiate(stoneIcon, slots[i].transform, false);
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = collectedStones.ToString();
                    GameObject.Find("StoneIcon(Clone)").transform.position = slots[i].transform.position;
                }
                else if(inventoryItems[i] == "Mushroom")
                {
                    Instantiate(mushroomIcon, slots[i].transform, false);
                    GameObject.Find("ItemAmount" + i.ToString()).GetComponent<TextMeshProUGUI>().text = collectedMushrooms.ToString();
                    GameObject.Find("MushroomIcon(Clone)").transform.position = slots[i].transform.position;
                }
            }
        }
    }
}
