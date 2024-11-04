using UnityEngine;

public class GravityToggleColor : MonoBehaviour
{
    public float minBrightness = 0.5f;
    public float maxBrightness = 1.0f;
    public float speed = 3.5f;
    private float timer = 0.0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1.0f, 0.5f, 0.0f) * Mathf.Lerp(minBrightness, maxBrightness, (Mathf.Sin(timer) + 1) / 2);
            timer += Time.deltaTime * speed;
        }
    }
}
