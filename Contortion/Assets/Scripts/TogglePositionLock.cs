using UnityEngine;

public class TogglePositionLock : MonoBehaviour
{
    private Rigidbody2D rb;
    public RotatePlayArea rotatePlayArea;
    public GameObject r;
    private ReverseGravity reverseGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reverseGravity = GetComponent<ReverseGravity>();

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
        if (rotatePlayArea == null) return;

        bool isReverseCrusher = rb.gameObject.CompareTag("Reverse_Crusher") && reverseGravity != null;

        if (isReverseCrusher)
        {

            bool shouldFreeze = rotatePlayArea.isRotating || !reverseGravity.IsToggleUsed;
            rb.constraints = shouldFreeze ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;

            if (rotatePlayArea.isRotating)
            {
                if (!reverseGravity.IsToggleUsed)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                else
                {


                }

            }
        }
        else
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