using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillerLevel3 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Respawn();
        }

        if (collision.gameObject.tag == "Crusher")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Rigidbody2D crusherRb = collision.collider.attachedRigidbody;

            float forceThreshold = 100f;
            float crusherVelocityMagnitude = crusherRb.velocity.magnitude;
            float collisionForce = crusherVelocityMagnitude * crusherRb.mass;

            Debug.Log("Force: " + collisionForce + "Vel:" + crusherVelocityMagnitude);

            if (collisionForce > forceThreshold && crusherVelocityMagnitude > 0)
            {
                Debug.Log("High force collision detected with Crusher. Force: " + collisionForce + "Vel:" + crusherVelocityMagnitude);
                Respawn();
            }
        }
    }

    void Respawn()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
