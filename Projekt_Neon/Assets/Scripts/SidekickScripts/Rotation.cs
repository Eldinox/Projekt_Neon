using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private GameObject _player;
    private Player GetPlayer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
         _player = GameObject.Find("Player");
        GetPlayer = _player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(GetPlayer.isMoving)
        //{
        //    Debug.Log("isMoving");

        //}
        //else
        //{
        //    Debug.Log("is not");
        //}

    }
}
