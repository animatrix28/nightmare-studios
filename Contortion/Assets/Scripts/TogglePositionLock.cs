using UnityEngine;

public class TogglePositionLock : MonoBehaviour
{
    private Rigidbody2D rb;
    public RotatePlayArea rotatePlayArea;
    public GameObject r;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (r != null)
        {
            rotatePlayArea = r.GetComponentInParent<RotatePlayArea>();
        }

        if (rotatePlayArea)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    void Update()
    {
        if (rotatePlayArea != null)
        {
            if (rotatePlayArea.isRotating)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
            else if (rb.velocity.magnitude < 0.1f)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
