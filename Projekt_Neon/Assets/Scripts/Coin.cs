using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour
{
    public int number;
    //private TextMeshProUGUI coincount;

    void Start()
    {
        //coincount = GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if(collision.tag == "Player")
    	{
    		collision.GetComponent<Player>().UpdateCoins(number);
            //int prevAmount = int.Parse(coincount.text);
            //int newAmount = prevAmount + 1;
            //coincount.text = "hi";//newAmount.ToString();
    		Destroy(gameObject);
    	}
    }
}
