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



            if (collisionForce > forceThreshold && crusherVelocityMagnitude > 0)
            {

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
