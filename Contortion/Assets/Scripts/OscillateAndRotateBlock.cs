using UnityEngine;

public class OscillateAndRotateBlock : MonoBehaviour
{
    public float speed = 2f;
    public float boundary = 0.5f;
    public bool moveAlongX = true;

    private Vector3 startPosition;

    private bool isAttached = false;

    private Vector3 originalScale;



    public GameObject r;

    public RotatePlayArea rotatePlayArea;
    private Transform playerOriginalParent;
    public GameObject player;
    void Start()
    {
        startPosition = transform.position;
        rotatePlayArea = r.GetComponentInParent<RotatePlayArea>();
    }

    void Update()
    {



        // Calculate the oscillating movement within the boundary
        float oscillation = Mathf.PingPong(Time.time * speed, boundary * 2) - boundary;


        if (Input.GetKeyDown(KeyCode.F) && rotatePlayArea.isRotating)
        {


            Vector3 moveDirection = transform.TransformDirection(Vector3.up) * oscillation;
            transform.position = startPosition + moveDirection;

        }
        else
        {

            Vector3 moveDirection = transform.TransformDirection(Vector3.right) * oscillation;
            transform.position = startPosition + moveDirection;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttached && !rotatePlayArea.isRotating)
        {
            isAttached = true;
            originalScale = collision.transform.localScale;
            playerOriginalParent = collision.transform.parent;
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAttached && !rotatePlayArea.isRotating)
        {
            isAttached = false;
            collision.transform.SetParent(playerOriginalParent);
            collision.transform.localScale = originalScale;
        }
    }


    void OnEnable()
    {
        RotatePlayArea.OnRotationStart += HandleRotationStart;
    }

    void OnDisable()
    {
        RotatePlayArea.OnRotationStart -= HandleRotationStart;
    }

    void HandleRotationStart()
    {
        if (isAttached)
        {

            isAttached = false;
            player.transform.SetParent(playerOriginalParent);
            player.transform.localScale = originalScale;
        }
    }
}