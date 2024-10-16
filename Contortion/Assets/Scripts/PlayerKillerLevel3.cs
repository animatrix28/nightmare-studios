using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillerLevel3 : MonoBehaviour
{
    // Start is called before the first frame update

     public Transform respawnTransform; 
    public Transform playAreaTransform;
public Transform movingblock_transform; 
    private Vector3 playAreaEulerAngles; 

     private Vector3 movingblock_rot;
private Vector2 movingblock_pos;
private Vector2 respawnPosition;
    
    void Start()
    {
           respawnPosition = respawnTransform.localPosition; 
        playAreaEulerAngles = playAreaTransform.eulerAngles; 
        movingblock_pos = movingblock_transform.localPosition;
        movingblock_rot = movingblock_transform.eulerAngles;

        
    }


        void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {

            
         
        resetEverything();
        }

        // if(collision.gameObject.tag == "Crusher")
        // {
        //      Rigidbody2D crusherRb = collision.collider.attachedRigidbody;
           
        //     float crusherVelocityMagnitude = crusherRb.velocity.magnitude;

        // float forceThreshold = 100f; 

        //  float collisionForce = crusherVelocityMagnitude * crusherRb.mass;
        //          Debug.Log("Force: " + collisionForce + "Vel:" + crusherVelocityMagnitude );

        //          Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // if (collisionForce > forceThreshold && crusherVelocityMagnitude > 0)
        // {
        
        //     Debug.Log("High force collision detected with Crusher. Force: " + collisionForce + "Vel:" +crusherVelocityMagnitude );

        // resetEverything();
            
        // }



        // }
    }

    void resetEverything()
    {

       transform.localPosition = respawnPosition; 
           playAreaTransform.eulerAngles = playAreaEulerAngles;
           movingblock_transform.eulerAngles = movingblock_rot;
         
           movingblock_transform.localPosition = movingblock_pos;
        


         
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0; 

            }


    }

}
