using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerKiller : MonoBehaviour
{
    public GameObject deathMessageUI;
    public static string CauseOfDeath = "Unknown";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Debug.Log("Death Cause: Spikes");
            CauseOfDeath = "Spikes";
            StartCoroutine(RespawnWithDelay());
        }

        if (collision.gameObject.tag == "Crusher")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Rigidbody2D crusherRb = collision.collider.attachedRigidbody;

            float forceThreshold = 100f;
            float crusherVelocityMagnitude = crusherRb.velocity.magnitude;
            float collisionForce = crusherVelocityMagnitude * crusherRb.mass;



            if (collisionForce > forceThreshold && crusherVelocityMagnitude > 0)
            {
                Debug.Log("Death Cause: Crusher");
                Debug.Log("High force collision detected with Crusher. Force: " + collisionForce + "Vel:" + crusherVelocityMagnitude);
                CauseOfDeath = "Crusher";
                StartCoroutine(RespawnWithDelay());
            }
        }
    }

    IEnumerator RespawnWithDelay()
    {
        Time.timeScale = 0;
        deathMessageUI.SetActive(true);


        yield return new WaitForSecondsRealtime(2);

        Time.timeScale = 1;
        deathMessageUI.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}