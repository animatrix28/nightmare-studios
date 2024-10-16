using UnityEngine;

public class TogglePositionLock : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FreezePosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Only unfreeze if currently frozen
            UnfreezePosition();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            FreezePosition();
        }
    }

    void FreezePosition()
    {
        rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
    }

    void UnfreezePosition()
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}