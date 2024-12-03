using UnityEngine;

public class GravityToggleColor : MonoBehaviour
{
    public float minBrightness = 0.5f;
    public float maxBrightness = 1.5f;
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
            spriteRenderer.color = new Color(0.8326389f, 0f, 0.9921569f) * Mathf.Lerp(minBrightness, maxBrightness, (Mathf.Sin(timer) + 1) / 2);
            timer += Time.deltaTime * speed;
        }
    }
}
