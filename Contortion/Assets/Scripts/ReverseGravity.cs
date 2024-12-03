using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool IsToggleUsed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsToggleUsed = false;
    }

    public void ReverseObjectGravity()
    {
        if (rb != null)
        {
            rb.gravityScale = -rb.gravityScale;
            IsToggleUsed = true;

        }
    }
}