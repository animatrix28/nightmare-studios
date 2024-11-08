using UnityEngine;

public class OscillateAndRotateBlock : MonoBehaviour
{
    public float speed = 2f; // Speed of the block
    public float boundary = 0.5f; // Boundary for movement
    public bool moveAlongX = true; // Toggle for movement along the X or Y axis

    private Vector3 startPosition;
    private bool isFlipped = false; // Tracks if the block is flipped

    void Start()
    {
        startPosition = transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0); // Start with no rotation
    }

    void Update()
    {
        // Flip screen orientation and rotate block when "F" is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            //transform.rotation = Quaternion.Euler(0, 0, isFlipped ? 180f : 0f); // Toggle between 0 and 180 degrees
	    //isFlipped = !isFlipped;      
	    moveAlongX = !moveAlongX;  
	}

        // Calculate the oscillating movement within the boundary
        float oscillation = Mathf.PingPong(Time.time * speed, boundary * 2) - boundary;

        // Apply movement along the selected axis
        if (moveAlongX)
        {
            transform.position = new Vector3(startPosition.x + oscillation, startPosition.y, startPosition.z);
        }
        else
        {
            transform.position = new Vector3(startPosition.x, startPosition.y + oscillation, startPosition.z);
        }
    }
}