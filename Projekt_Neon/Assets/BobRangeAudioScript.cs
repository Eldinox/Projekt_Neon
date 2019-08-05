using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobRangeAudioScript : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioData;
    public AudioClip FootSteps;
    public AudioClip LightAttack;
    public AudioClip HeavyAttack;
    public AudioClip Jump;
    public AudioClip Landing;
    public AudioClip Uppercut;


    // Start is called before the first frame update
    void Start()
    {
      audioData = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void footsteps ()
    {
        audioData.clip = FootSteps;
        audioData.Play(0);
    }

    public void jump ()
    {
        audioData.clip = Jump;
        audioData.Play(0);
    }
    public void landing ()
    {
        audioData.clip = Landing;
        audioData.Play(0);
    }
    public void heavyAttack ()
    {
        audioData.clip = HeavyAttack;
        audioData.Play(0);
    }
    public void uppercut ()
    {
        audioData.clip = Uppercut;
        audioData.Play(0);
    }
     public void lightAttack ()
    {
        audioData.clip = LightAttack;
        audioData.Play(0);
    }
}
