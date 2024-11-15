using UnityEngine;

public class TogglePositionLock : MonoBehaviour
{
    private Rigidbody2D rb;
    public RotatePlayArea rotatePlayArea;
    public GameObject r;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Attempt to get RotatePlayArea from the parent of `r`
        if (r != null)
        {
            rotatePlayArea = r.GetComponentInParent<RotatePlayArea>();
        }

        if (rotatePlayArea == null)
        {
            Debug.LogError("RotatePlayArea component not found on the parent of the assigned GameObject.");
        }
        else
        {
            FreezePosition();
        }
    }


    void Update()
    {

        if (rotatePlayArea.isRotating)
        {

            UnfreezePosition();

        }



    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
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
