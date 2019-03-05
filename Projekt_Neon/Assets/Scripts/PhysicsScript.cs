using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsScript : MonoBehaviour
{
    protected Vector2 velocity;
    protected Rigidbody2D rb;
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D>hitBufferList = new List<RaycastHit2D>(16);
    public float gravityModifier = 1f;
    public float minGroundNormalY = .65f;
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        Movement(move);
    }

    void Movement(Vector2 move)
    {
        float distance = move.magnitude;
        if(distance > minMoveDistance)
        {
            int count = rb.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for(int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            for(int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y > minGroundNormalY)
                {
                    
                }
            }
        }
        rb.position = rb.position + move;
    }
}
