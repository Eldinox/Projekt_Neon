using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    // Start is called before the first frame update
AudioSource audio1;
public AudioSource audio3;
public AudioSource audio2;
private bool playonce ;
void Start()
{
    audio1 = GetComponent<AudioSource>();
    playonce = false;
}

void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicSoucre");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

private void Update() {
    if(SceneManager.GetActiveScene().name == "Level_7 (Boss)" )
    {
        audio2.Stop();
        audio1.Stop();
        transform.position = new Vector3 (132f,4.8f,0f);
    
    }
}

void OnTriggerExit2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().name == "Level_1_22" && col.gameObject.tag == "Player" && audio1.isPlaying)
        {
            StartCoroutine(FadeOut(audio1,6f));
            audio2.Play();

        }
    }
void OnTriggerEnter2D(Collider2D col)
    {
         if (SceneManager.GetActiveScene().name == "Level_7 (Boss)" && col.gameObject.tag == "Player" && playonce == false )
        {
            audio3.Play();
            playonce = true;
        }
    }

 private IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }

}




    

