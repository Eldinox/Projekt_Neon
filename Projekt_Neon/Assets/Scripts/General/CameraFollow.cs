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
}
