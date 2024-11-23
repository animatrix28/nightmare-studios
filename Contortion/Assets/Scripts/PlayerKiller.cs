using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerKiller : MonoBehaviour
{
    [Header("Death and Respawn")]
    public GameObject deathMessageUI;
    public static string CauseOfDeath = "Unknown";
    private bool isRespawning = false;

    [Header("Crush Sensors")]
    public GameObject topSensor;
    public GameObject bottomSensor;
    public GameObject leftSensor;
    public GameObject rightSensor;

    [Header("Crush Settings")]
    [SerializeField] private float minCrusherVelocity = 2f;

    [Header("Death Camera")]
    public float zoomAmount = 4f;
    public float zoomSpeed = 2f;
    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    private float originalCameraSize;
    private Vector3 deathPosition;

    // Sensor states
    private bool isTopTouching = false;
    private bool isBottomTouching = false;
    private bool isLeftTouching = false;
    private bool isRightTouching = false;

    private string topTouchingTag = "";
    private string bottomTouchingTag = "";
    private string leftTouchingTag = "";
    private string rightTouchingTag = "";

    private Dictionary<GameObject, Rigidbody2D> crusherRigidbodies = new Dictionary<GameObject, Rigidbody2D>();
    private Rigidbody2D playerRigidbody;

    private class CrushSensor : MonoBehaviour
    {
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

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        // Store camera references
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.transform.position;
            originalCameraSize = mainCamera.orthographicSize;
        }

        // Create sensors if not set in inspector
        if (topSensor == null) CreateSensor("TopSensor", new Vector2(0, 1f), new Vector2(0.2f, 0.1f));
        if (bottomSensor == null) CreateSensor("BottomSensor", new Vector2(0, -1f), new Vector2(0.2f, 0.1f));
        if (leftSensor == null) CreateSensor("LeftSensor", new Vector2(-1f, 0), new Vector2(0.1f, 0.2f));
        if (rightSensor == null) CreateSensor("RightSensor", new Vector2(1f, 0), new Vector2(0.1f, 0.2f));
    }

    bool CheckVerticalCrush()
    {
        if (!(isTopTouching && isBottomTouching)) return false;

        bool crusherOnTop = topTouchingTag == "Crusher" && bottomTouchingTag == "Ground";
        bool crusherOnBottom = topTouchingTag == "Ground" && bottomTouchingTag == "Crusher";

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
            float crusherVelocity = Mathf.Abs(crusherRb.velocity.y);
            Debug.Log($"Crusher absolute vertical velocity: {crusherVelocity}");
            return crusherVelocity >= minCrusherVelocity;
        }
        return false;
    }

    bool CheckHorizontalCrush()
    {
        if (!(isLeftTouching && isRightTouching)) return false;

        bool crusherOnLeft = leftTouchingTag == "Crusher" && rightTouchingTag == "Ground";
        bool crusherOnRight = leftTouchingTag == "Ground" && rightTouchingTag == "Crusher";

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
            Debug.Log($"Crusher absolute velocity: {crusherVelocity}");
            return crusherVelocity >= minCrusherVelocity; //returns true 
        }
        return false;
    }

    void CreateSensor(string name, Vector2 position, Vector2 size)
    {
        GameObject sensor = new GameObject(name);
        sensor.transform.parent = transform;
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

        if (verticalCrush || horizontalCrush)
        {
            deathPosition = transform.position;
            CauseOfDeath = "Crusher";
            StartRespawnSequence();
        }
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
        Time.timeScale = 0;
        deathMessageUI.SetActive(true);

        if (mainCamera != null)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = mainCamera.transform.position;
            float startSize = mainCamera.orthographicSize;
            Vector3 targetPosition = new Vector3(deathPosition.x, deathPosition.y, mainCamera.transform.position.z);
            float targetSize = originalCameraSize / zoomAmount;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.unscaledDeltaTime * zoomSpeed;
                float t = Mathf.SmoothStep(0, 1, elapsedTime);

                mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

                yield return null;
            }
        }

        float waitTime = 5f;
        float elapsedWait = 0f;
        while (elapsedWait < waitTime)
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                break;
            }
            elapsedWait += Time.unscaledDeltaTime;
            yield return null;
        }

        if (mainCamera != null)
        {
            mainCamera.transform.position = originalCameraPosition;
            mainCamera.orthographicSize = originalCameraSize;
        }

        deathMessageUI.SetActive(false);
        Time.timeScale = 1;

        RestartGame();
        isRespawning = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}