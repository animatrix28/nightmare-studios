using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerKiller : MonoBehaviour
{
    public GameObject deathMessageUI;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
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
<<<<<<< Updated upstream

                Respawn();
=======
                Debug.Log("High force collision detected with Crusher. Force: " + collisionForce + "Vel:" + crusherVelocityMagnitude);
                StartCoroutine(RespawnWithDelay());
>>>>>>> Stashed changes
            }
        }
    }

    IEnumerator RespawnWithDelay()
    {
        Time.timeScale = 0;
        deathMessageUI.SetActive(true);

        yield return new WaitForSecondsRealtime(2);
        
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
