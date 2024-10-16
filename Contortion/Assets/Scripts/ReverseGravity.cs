using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ReverseObjectGravity()
    {
        if (rb != null)
        {
            rb.gravityScale = -rb.gravityScale;
        }
    }
}