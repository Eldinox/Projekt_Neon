using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + yOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
        	float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        	float clampedY = Mathf.Clamp(playerTransform.position.y + yOffset, minY, maxY);

        	transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
