using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackDisplay : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    public bool getHitAnimation;
    public bool getHitColoring;
    public bool healthBars;
    public bool damageNumberDisplay;
    public bool hitSparks;
}
