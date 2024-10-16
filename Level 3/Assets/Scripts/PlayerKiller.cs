using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public Transform respawnTransform; // Renamed for consistency
    public Transform playAreaTransform; // Renamed for consistency

    private Vector3 respawnPosition; // Corrected type
    private Vector3 playAreaEulerAngles; // Corrected type

    void Start()
    {
        // Store initial positions and rotations using correct Vector3 types
        respawnPosition = respawnTransform.localPosition; // Assuming you want local position
        playAreaEulerAngles = playAreaTransform.eulerAngles; // Assuming you want local rotation
        Debug.Log(playAreaTransform.localEulerAngles);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {

            Debug.Log(playAreaTransform.localEulerAngles);
            // Reset player position and play area rotation
            transform.localPosition = respawnPosition; // Use localPosition if originally intended
           playAreaTransform.eulerAngles = playAreaEulerAngles;
 // Use localEulerAngles to match Start()

            // If using Rigidbody2D and want to reset physics properties
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Resets velocity
                rb.angularVelocity = 0; // Resets angular velocity
            }
        }
    }
}
