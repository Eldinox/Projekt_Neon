using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public float smoothSpeed = 2f;
    public float offsetValue = 10f;
    public Vector3 offset = new Vector3(0, 7, -1);
    
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = playerTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        if(Input.GetKey(KeyCode.Keypad8) || Input.GetAxis("RightStickY") < 0)offset.y = offsetValue + offsetValue/2;
        else if(Input.GetKey(KeyCode.Keypad2) || Input.GetAxis("RightStickY") > 0)offset.y = -offsetValue + offsetValue/2;
        else offset.y = 7;
        if(Input.GetKey(KeyCode.Keypad6) || Input.GetAxis("RightStickX") > 0)offset.x = offsetValue;
        else if(Input.GetKey(KeyCode.Keypad4) || Input.GetAxis("RightStickX") < 0)offset.x = -offsetValue;
        else offset.x = 0;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /*public Transform playerTransform;
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
    }*/
}
