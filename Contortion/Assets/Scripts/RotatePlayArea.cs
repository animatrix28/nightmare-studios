using UnityEngine;

public class RotatePlayArea : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public bool isRotating = false;
    private float targetAngle;

    private float angleBefore;

    private Vector3 originalScale;

    public Transform skull;

    public Rigidbody2D rb;
    public Transform flag;
    public Transform player;

    public Transform Fkey;




    public static event System.Action OnRotationStart;



    void Update()
    {
        angleBefore = transform.eulerAngles.z;

        if (Input.GetKeyDown(KeyCode.F) && !isRotating)
        {
            targetAngle = angleBefore - 90f;
            isRotating = true;
            OnRotationStart?.Invoke();
        }

        if (isRotating)
        {
            RotateArea();
        }
    }

    void RotateArea()
    {
        float step = rotationSpeed * Time.deltaTime;
        float angle = Mathf.MoveTowardsAngle(angleBefore, targetAngle, step);

        transform.eulerAngles = new Vector3(0, 0, angle);
        flag.Rotate(Vector3.back * (angle - angleBefore));
        player.Rotate(Vector3.back * (angle - angleBefore));
        if (skull)
        {
            skull.Rotate(Vector3.back * (angle - angleBefore));
        }

        if (Fkey)
        {
            Fkey.Rotate(Vector3.back * (angle - angleBefore));

        }



        if (Mathf.Approximately(angle, targetAngle))
        {
            isRotating = false;
        }
    }
}

