using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;



public class PlayerKiller : MonoBehaviour
{
    [Header("Death and Respawn")]
    public GameObject deathMessageUI;
    public GameObject pauseMenuCanvas;


    public static string CauseOfDeath = "Unknown";
    private bool isRespawning = false;



    [Header("Crush Sensors")]
    public GameObject topSensor;
    public GameObject bottomSensor;
    public GameObject leftSensor;
    public GameObject rightSensor;

    [Header("Crush Settings")]
    [SerializeField] private float minCrusherVelocity;
    [SerializeField] private float hminCrusherVelocity;

    [Header("Death Camera")]
    public float zoomAmount = 4f;
    public float zoomSpeed = 2f;
    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    private float originalCameraSize;
    private Vector3 deathPosition;

    private Transform player;

    // Sensor states
    private bool isTopTouching = false;
    private bool isBottomTouching = false;
    private bool isLeftTouching = false;
    private bool isRightTouching = false;

    private string topTouchingTag = "";
    private string bottomTouchingTag = "";
    private string leftTouchingTag = "";
    private string rightTouchingTag = "";

    private Vector3 playerSize;

    private Dictionary<GameObject, Rigidbody2D> crusherRigidbodies = new Dictionary<GameObject, Rigidbody2D>();
    private Rigidbody2D playerRigidbody;



    private class CrushSensor : MonoBehaviour
    {
        private bool isStaying;
        private PlayerKiller parentScript;

        void Start()
        {
            parentScript = GetComponentInParent<PlayerKiller>();

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Crusher"))
            {
                if (other.CompareTag("Crusher"))
                {
                    Rigidbody2D rb = other.attachedRigidbody;
                    if (rb != null)
                    {
                        parentScript.crusherRigidbodies[gameObject] = rb;
                    }
                }
                parentScript.UpdateSensorState(gameObject.name, true, other.tag);
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Crusher"))
            {
                if (other.CompareTag("Crusher"))
                {
                    Rigidbody2D rb = other.attachedRigidbody;
                    if (rb != null)
                    {
                        parentScript.crusherRigidbodies[gameObject] = rb;
                    }
                }
                parentScript.UpdateSensorState(gameObject.name, true, other.tag);
            }
        }





        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Crusher"))
            {
                if (other.CompareTag("Crusher"))
                {
                    parentScript.crusherRigidbodies.Remove(gameObject);
                }
                parentScript.UpdateSensorState(gameObject.name, false, "");
            }
        }
    }



    bool CheckVerticalCrush()
    {
        if (!(isTopTouching && isBottomTouching)) return false;

        bool crusherOnTop = (topTouchingTag == "Crusher" && bottomTouchingTag == "Ground");
        bool crusherOnBottom = (topTouchingTag == "Ground" && bottomTouchingTag == "Crusher");

        if (!crusherOnTop && !crusherOnBottom) return false;

        // Get the crusher's rigidbody
        Rigidbody2D crusherRb = null;
        if (crusherOnTop && crusherRigidbodies.ContainsKey(topSensor))
        {
            crusherRb = crusherRigidbodies[topSensor];
        }
        else if (crusherOnBottom && crusherRigidbodies.ContainsKey(bottomSensor))
        {
            crusherRb = crusherRigidbodies[bottomSensor];
        }

        // Check crusher's absolute velocity when there's ground contact
        if (crusherRb != null)

        {

            if (crusherOnBottom && crusherRb.velocity.y > 0)
            {
                float crusherVelocity = Mathf.Abs(crusherRb.velocity.y);
                Debug.Log($"Crusher absolute vertical velocity: {crusherRb.velocity.y}");

                return crusherVelocity >= minCrusherVelocity;
            }
            else if (crusherOnTop && crusherRb.velocity.y < 0)
            {
                float crusherVelocity = Mathf.Abs(crusherRb.velocity.y);
                Debug.Log($"Crusher absolute vertical velocity: {crusherRb.velocity.y}");

                return crusherVelocity >= minCrusherVelocity;


            }
        }
        return false;
    }

    bool CheckHorizontalCrush()
    {
        if (!(isLeftTouching && isRightTouching)) return false;

        bool crusherOnLeft = (leftTouchingTag == "Crusher" && rightTouchingTag == "Ground");
        bool crusherOnRight = (leftTouchingTag == "Ground" && rightTouchingTag == "Crusher");

        if (!crusherOnLeft && !crusherOnRight) return false;

        // Get the crusher's rigidbody
        Rigidbody2D crusherRb = null;
        if (crusherOnLeft && crusherRigidbodies.ContainsKey(leftSensor))
        {
            crusherRb = crusherRigidbodies[leftSensor];
        }
        else if (crusherOnRight && crusherRigidbodies.ContainsKey(rightSensor))
        {
            crusherRb = crusherRigidbodies[rightSensor];
        }

        // Check crusher's absolute velocity when there's ground contact
        if (crusherRb != null)
        {
            float crusherVelocity = Mathf.Abs(crusherRb.velocity.x);
            // Debug.Log($"Crusher absolute velocity: {crusherVelocity}");
            return crusherVelocity >= hminCrusherVelocity; //returns true 
        }
        return false;
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        playerSize = player.transform.localScale;

        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        float colliderHeight = capsuleCollider.bounds.size.y;
        float colliderWidth = capsuleCollider.bounds.size.x;

        // Store camera references
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.transform.position;
            originalCameraSize = mainCamera.orthographicSize;
        }


        // Create sensors right at the collider edges
        if (topSensor == null) CreateSensor("TopSensor", new Vector2(0f, 0.27f), new Vector2(0.318402767f, 0.128026783f));
        if (bottomSensor == null) CreateSensor("BottomSensor", new Vector2(0f, -0.3068475f), new Vector2(0.227577209f, 0.0996543318f));
        if (leftSensor == null) CreateSensor("LeftSensor", new Vector2(-0.2320364f, 0f), new Vector2(0.05474677086f, 0.490971088f));
        if (rightSensor == null) CreateSensor("RightSensor", new Vector2(0.2320364f, 0f), new Vector2(0.05476083755f, 0.48587501f));
    }

    void CreateSensor(string name, Vector2 position, Vector2 size)
    {
        GameObject sensor = new GameObject(name);
        sensor.transform.parent = transform;

        // Position the sensor exactly at the edge of the collider
        sensor.transform.localPosition = position;

        BoxCollider2D collider = sensor.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = size;

        SpriteRenderer renderer = sensor.AddComponent<SpriteRenderer>();
        renderer.color = new Color(1, 0, 0, 0.3f);
        renderer.sortingOrder = 1;

        sensor.AddComponent<CrushSensor>();

        switch (name)
        {
            case "TopSensor": topSensor = sensor; break;
            case "BottomSensor": bottomSensor = sensor; break;
            case "LeftSensor": leftSensor = sensor; break;
            case "RightSensor": rightSensor = sensor; break;
        }
    }

    public void UpdateSensorState(string sensorName, bool isTouching, string touchingTag)
    {
        switch (sensorName)
        {
            case "TopSensor":
                isTopTouching = isTouching;
                topTouchingTag = isTouching ? touchingTag : "";
                break;
            case "BottomSensor":
                isBottomTouching = isTouching;
                bottomTouchingTag = isTouching ? touchingTag : "";
                break;
            case "LeftSensor":
                isLeftTouching = isTouching;
                leftTouchingTag = isTouching ? touchingTag : "";
                break;
            case "RightSensor":
                isRightTouching = isTouching;
                rightTouchingTag = isTouching ? touchingTag : "";
                break;
        }
        CheckForCrush();
    }

    void CheckForCrush()
    {
        bool verticalCrush = CheckVerticalCrush();
        bool horizontalCrush = CheckHorizontalCrush();


        if (verticalCrush)
        {
            deathPosition = transform.position;
            // Debug.Log("Vertical");
            CauseOfDeath = "Crusher";
            StartRespawnSequence();
        }
        // if (horizontalCrush)
        // {
        //     deathPosition = transform.position;
        //     Debug.Log("Horizontal");
        //     CauseOfDeath = "Crusher";
        //     StartRespawnSequence();

        // }
    }

    void Update()
    {
        if (isRespawning && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            deathPosition = transform.position;
            CauseOfDeath = "Spikes";
            StartRespawnSequence();
        }
    }

    void StartRespawnSequence()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnWithDelay());
        }
    }

    IEnumerator RespawnWithDelay()
    {
        isRespawning = true;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 0;
        deathMessageUI.SetActive(true);
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();






        if (mainCamera != null)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = mainCamera.transform.position;
            float startSize = mainCamera.orthographicSize;
            Vector3 targetPosition = new Vector3(deathPosition.x, deathPosition.y, mainCamera.transform.position.z); //keep camera's original z
            float targetSize = originalCameraSize / zoomAmount;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.unscaledDeltaTime * zoomSpeed;
                float t = Mathf.SmoothStep(0, 1, elapsedTime);

                mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);




                playerRenderer.color = Color.red;





                yield return null;
            }

        }


        yield return new WaitForSecondsRealtime(3);


        if (mainCamera != null)
        {
            mainCamera.transform.position = originalCameraPosition;
            mainCamera.orthographicSize = originalCameraSize;

        }

        deathMessageUI.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        RestartGame();
        isRespawning = false;

    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}