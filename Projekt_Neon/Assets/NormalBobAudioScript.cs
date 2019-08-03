using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBobAudioScript : MonoBehaviour
{
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void footsteps ()
    {
        Debug.Log("soundCheck");
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);

    }
}
