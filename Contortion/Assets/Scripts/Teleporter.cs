using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the teleport zone
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the destination
            other.transform.position = teleportDestination.position;
        }
    }
}
