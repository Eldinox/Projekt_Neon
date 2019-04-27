using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] pillars;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("Attack1", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack1()
    {
        anim.SetTrigger("PillarAttack1");
    }
}
