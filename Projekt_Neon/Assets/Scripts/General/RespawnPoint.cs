﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPoint : MonoBehaviour
{
    public GameObject fire;
    public AudioClip fireSound;

    private AudioSource FireAudioSource;

    private Scene activeScene;
    
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        if(GameObject.Find("Player").GetComponent<Player>().respawnPoint == activeScene.name)
        {
            fire.SetActive(true);
        }
        FireAudioSource = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FireAudioSource.clip = fireSound;
            FireAudioSource.Play(0);
            GameObject.Find("Player").GetComponent<Player>().respawnPoint = activeScene.name;
            fire.SetActive(true);
        }
    }
}
