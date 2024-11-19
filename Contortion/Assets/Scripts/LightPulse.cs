using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    public Light2D light2D;
    public float minIntensity = 4f;
    public float maxIntensity = 8f;
    public float flickerSpeed = 0.1f;  // How often to change intensity
    private float nextFlickerTime = 0f;

    void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }
    }

    void Update()
    {
        if (Time.time > nextFlickerTime)
        {
            // Get a random value between 0 and 1
            float randomValue = Random.value;
            light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, randomValue);

            // Set next flicker time
            nextFlickerTime = Time.time + flickerSpeed;
        }
    }
}