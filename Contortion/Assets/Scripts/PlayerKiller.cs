using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public Transform respawnTransform; 
    public Transform playAreaTransform; 

    public Transform block1Transform; 
    public Transform block2Transform; 

    private Vector3 respawnPosition; 

    private Vector2 block1pos; 
    private Vector2 block2pos;

    private Vector3 block1rot;
    private Vector3 block2rot;
    private Vector3 playAreaEulerAngles; 

    void Start()
    {
        respawnPosition = respawnTransform.localPosition; 
        playAreaEulerAngles = playAreaTransform.eulerAngles; 
        block1pos = block1Transform.localPosition;
        block1rot = block1Transform.eulerAngles;
        block2pos = block2Transform.localPosition;
        block2rot = block2Transform.eulerAngles;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            resetEverything();
        }

        if(collision.gameObject.tag == "Crusher")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Rigidbody2D crusherRb = collision.collider.attachedRigidbody;
           
            float forceThreshold = 100f;
            float crusherVelocityMagnitude = crusherRb.velocity.magnitude;
            float collisionForce = crusherVelocityMagnitude * crusherRb.mass;

            Debug.Log("Force: " + collisionForce + "Vel:" + crusherVelocityMagnitude );

            if (collisionForce > forceThreshold && crusherVelocityMagnitude > 0)
            {
                Debug.Log("High force collision detected with Crusher. Force: " + collisionForce + "Vel:" +crusherVelocityMagnitude );
                resetEverything();
            }
        }
    }

    void resetEverything()
    {
        transform.localPosition = respawnPosition; 
        playAreaTransform.eulerAngles = playAreaEulerAngles;
        block1Transform.eulerAngles = block1rot;
        block2Transform.eulerAngles = block2rot;
        block1Transform.localPosition = block1pos;
        block2Transform.localPosition = block2pos;
         
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0; 
        }
    }
}
