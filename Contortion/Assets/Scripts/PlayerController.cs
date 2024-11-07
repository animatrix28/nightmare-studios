using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100.0f;
    public Rigidbody2D rb;
    public bool isGrounded;

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(move * speed, rb.velocity.y);
        rb.velocity = movement;
    }
}