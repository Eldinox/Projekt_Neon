using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplay : MonoBehaviour
{
    Image fillImg;
    private float waitTime;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        fillImg = this.GetComponent<Image>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / waitTime;
        }
    }

    public void StartCD(float cd)
    {
        waitTime = cd;
        time = cd;
    }
}
