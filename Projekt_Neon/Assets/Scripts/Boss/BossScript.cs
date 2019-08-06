using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] pillars;
    public GameObject[] particleSystems;

    private GameObject[] animatedPillars;
    private GameObject[] animatedPillars2;
    private Animator anim;

    [HideInInspector]
    public Transform player;

    public int state;
    private GameObject pillar1;
    private int attackCount;
    public int eyeCountDestroy ;
    private AudioSource BossAudioSource;
    public AudioClip pillarSound;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        BossAudioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animatedPillars = GameObject.FindGameObjectsWithTag("BossPillar");
        animatedPillars2 = GameObject.FindGameObjectsWithTag("BossPillar2");
        attackCount = 0;
        foreach (var item in animatedPillars)
        {
            item.SetActive(false);
        }
        foreach (var item in animatedPillars2)
        {
            item.SetActive(false);
        }
        /*Invoke("System1", 3);
        Invoke("Attack1", 5);*/
    }

    void Update()
    {
        if(state == 0)
        {
            if(Vector2.Distance(transform.position, player.position) < 70)
            {
                //anim.SetTrigger("StartFight");
                state = 2;
            }
        }
        if(state == 2)
        {
            Invoke("System1", 3);
            Invoke("Attack1", 5);
            state = 3;
        }
        if(attackCount == 3)
        {
            attackCount = 0;
            anim.SetTrigger("LongAttack");
        }
        Debug.Log("Eye Destroy "+eyeCountDestroy);
        if(eyeCountDestroy == 3)
        {
            eyeCountDestroy = -1;
            anim.SetTrigger("LastHit");
            StartCoroutine(BossDead(anim));
            GameObject.Find("Limit").SetActive(false);
            state = 4;
        }

 {
    // Avoid any reload.
 }
    }
    
    private IEnumerator BossDead(Animator anim)
    {
        yield return new WaitForSeconds(5);
        anim.enabled = false;

        particleSystems[0].SetActive(false);
        particleSystems[1].SetActive(false);
        particleSystems[2].SetActive(false);
        particleSystems[3].SetActive(false);

        foreach (var item in animatedPillars)
        {
            item.SetActive(false);
        }
        foreach (var item in animatedPillars2)
        {
            item.SetActive(false);
        }
    }
    private void Attack1()
    {
        BossAudioSource.clip = pillarSound;
        BossAudioSource.Play(0);
        if(eyeCountDestroy >= 0)
        {
            anim.SetTrigger("PillarAttack1");
            GameObject.Find("Umgebung").GetComponent<Animator>().SetTrigger("shake");
            particleSystems[0].SetActive(false);
            particleSystems[2].SetActive(false);
            foreach (var item in animatedPillars)
            {
                item.SetActive(true);
            }
             foreach (var item in animatedPillars2)
            {
                item.SetActive(false);
            }
            attackCount++;
            Invoke("System2", 3);   
            Invoke("Attack2", 5);
            Debug.Log("attak1");
        }
    }
    private void Attack2()
    {
        BossAudioSource.clip = pillarSound;
        BossAudioSource.Play(0);
        if(eyeCountDestroy >= 0)
        {
            anim.SetTrigger("PillarAttack2");
            GameObject.Find("Umgebung").GetComponent<Animator>().SetTrigger("shake");
            particleSystems[1].SetActive(false);
            particleSystems[3].SetActive(false);
            foreach (var item in animatedPillars)
            {
                item.SetActive(false);
            }
             foreach (var item in animatedPillars2)
            {
                item.SetActive(true);
            }
            attackCount++;

            Invoke("System1", 3);
            Invoke("Attack1", 5);
            Debug.Log("attak2");
        }
    }

    private void System1()
    {
        particleSystems[0].SetActive(true);
        particleSystems[2].SetActive(true);
    }
    private void System2()
    {
        particleSystems[1].SetActive(true);
        particleSystems[3].SetActive(true);
    }
}
