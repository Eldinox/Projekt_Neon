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
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        Invoke("System1", 3);
        Invoke("Attack1", 5);
    }

    void Update()
    {
        if(state == 0)
        {
            if(Vector2.Distance(transform.position, player.position) < 70)
            {
                anim.SetTrigger("StartFight");
                state = 1;
            }
        }
        if(state == 2)
        {
            Invoke("System1", 3);
            Invoke("Attack1", 5);
        }
        if(attackCount == 8)
        {
            attackCount = 0;
            anim.SetTrigger("LongAttack");
        }
    }
    
    private void Attack1()
    {
        anim.SetTrigger("PillarAttack1");
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
    }
    private void Attack2()
    {
        anim.SetTrigger("PillarAttack2");
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
