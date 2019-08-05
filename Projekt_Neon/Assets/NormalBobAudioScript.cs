using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBobAudioScript : MonoBehaviour
{
    AudioSource audioData;
    public AudioClip FootSteps;
    public AudioClip LightAttack;
    public AudioClip HeavyAttack;
    public AudioClip Jump;
    public AudioClip Landing;

    // Start is called before the first frame update
    void Start()
    {
      audioData = GameObject.Find("Player").GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void lightAttack ()
    {
        audioData.clip = LightAttack;
        audioData.Play(0);
    }
}
