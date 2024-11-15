using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerKiller : MonoBehaviour
{
    public GameObject deathMessageUI;
    public static string CauseOfDeath = "Unknown";
    private bool isRespawning = false; // Flag to track if respawn is in progress

    void Update()
    {
        // Allow restart while the death message is displayed
        if (isRespawning && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            CauseOfDeath = "Spikes";
            StartRespawnSequence();
        }

        if (collision.gameObject.CompareTag("Crusher"))
        {
            Rigidbody2D crusherRb = collision.collider.attachedRigidbody;

            if (crusherRb != null) // Ensure the crusher has a Rigidbody2D
            {
                float forceThreshold = 100f;
                float crusherVelocityMagnitude = crusherRb.velocity.magnitude;
                float collisionForce = crusherVelocityMagnitude * crusherRb.mass;

                if (collisionForce > forceThreshold && crusherVelocityMagnitude > 0)
                {
                    Debug.Log("High force collision detected with Crusher. Force: " + collisionForce + " Vel: " + crusherVelocityMagnitude);
                    CauseOfDeath = "Crusher";
                    StartRespawnSequence();
                }
            }
        }
    }

    void StartRespawnSequence()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnWithDelay());
        }
    }

    IEnumerator RespawnWithDelay()
    {
        isRespawning = true;
        Time.timeScale = 0; // Pause the game
        deathMessageUI.SetActive(true);


        yield return new WaitForSecondsRealtime(5); // Wait for 2 real-time seconds

        Time.timeScale = 1; // Resume the game
        deathMessageUI.SetActive(false);

        RestartGame(); // Restart the game after the delay
        isRespawning = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Ensure time scale is reset
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload the current scene
    }
}
