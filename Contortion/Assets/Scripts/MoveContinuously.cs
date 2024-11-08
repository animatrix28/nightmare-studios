using UnityEngine;

public class OscillateAndRotateBlock : MonoBehaviour
{
    public float speed = 2f; // Speed of the block
    public float boundary = 0.5f; // Boundary for movement
    public bool moveAlongX = true; // Toggle for movement along the X or Y axis

    private Vector3 startPosition;

    public GameObject r;
    public RotatePlayArea rotatePlayArea;
    void Start()
    {
        startPosition = transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0); // Start with no rotation
        rotatePlayArea = r.GetComponent<RotatePlayArea>();
    }

    void Update()
    {

        // Flip screen orientation and rotate block when "F" is pressed
        // if (Input.GetKeyDown(KeyCode.F) && rotatePlayArea.isRotating)
        // {
        //transform.rotation = Quaternion.Euler(0, 0, isFlipped ? 180f : 0f); // Toggle between 0 and 180 degrees
        //isFlipped = !isFlipped;      
        // moveAlongX = !moveAlongX;
        // Debug.Log("Move along x" + moveAlongX);
        // }

        // Calculate the oscillating movement within the boundary
        float oscillation = Mathf.PingPong(Time.time * speed, boundary * 2) - boundary;

        // Apply movement along the selected axis
        // if (moveAlongX)
        if (Input.GetKeyDown(KeyCode.F) && rotatePlayArea.isRotating)
        {
            // Move the object in its local "up" direction, transformed to world space
            Vector3 moveDirection = transform.TransformDirection(Vector3.up) * oscillation;
            transform.position = startPosition + moveDirection;
        }
        else
        {
            // Move the object in its local "right" direction, transformed to world space
            Vector3 moveDirection = transform.TransformDirection(Vector3.right) * oscillation;
            transform.position = startPosition + moveDirection;
        }

    }
}