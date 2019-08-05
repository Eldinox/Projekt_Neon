using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerSystem : MonoBehaviour
{    AudioSource audioData;




    // Start is called before the first frame update
    void Start()
    {
      audioData = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void footsteps (AudioClip FootSteps)
    {
        audioData.clip = FootSteps;
        audioData.Play(0);
    }

    public void jump (AudioClip Jump)
    {
        audioData.clip = Jump;
        audioData.Play(0);
    }
    public void landing (AudioClip Landing)
    {
        audioData.clip = Landing;
        audioData.Play(0);
    }
    public void heavyAttack (AudioClip HeavyAttack)
    {
        audioData.clip = HeavyAttack;
        audioData.Play(0);
    }
    public void lightAttack (AudioClip LightAttack)
    {
        audioData.clip = LightAttack;
        audioData.Play(0);
    }


}
