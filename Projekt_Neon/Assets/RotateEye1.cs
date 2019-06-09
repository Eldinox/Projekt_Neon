using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEye1 : MonoBehaviour
{
    public float speed;
    public bool turnRight ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnRight)RotateRight();
        else RotateLeft();
        
    }

     void RotateLeft () 
     {
        transform.Rotate(0, 0, Time.deltaTime * speed, Space.World);
     }

     void RotateRight () 
     {
        transform.Rotate(0, 0, Time.deltaTime * -speed, Space.World);
     }
}
