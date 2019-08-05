using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBobAudioScript : MonoBehaviour
{
  AudioSource audioData;
    public AudioClip FootSteps;
    public AudioClip LightAttack;
    public AudioClip HeavyAttack;
    public AudioClip Jump;
    public AudioClip Landing;
    public AudioClip PowerWave;
    public AudioClip OverHeadAttack;

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
     public void lightAttack ()
    {
        audioData.clip = LightAttack;
        audioData.Play(0);
    }
     public void powerwave ()
    {
        audioData.clip = PowerWave;
        audioData.Play(0);
    }
     public void overHeadAttack ()
    {
        audioData.clip = OverHeadAttack;
        audioData.Play(0);
    }
}
