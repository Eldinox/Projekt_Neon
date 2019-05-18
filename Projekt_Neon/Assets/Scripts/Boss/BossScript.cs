using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] pillars;
    public GameObject[] particleSystems;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("System1", 3);
        Invoke("Attack1", 5);
    }
    
    private void Attack1()
    {
        anim.SetTrigger("PillarAttack1");
        particleSystems[0].SetActive(false);
        particleSystems[2].SetActive(false);
        Invoke("System2", 3);
        Invoke("Attack2", 5);
    }
    private void Attack2()
    {
        anim.SetTrigger("PillarAttack2");
        particleSystems[1].SetActive(false);
        particleSystems[3].SetActive(false);
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
