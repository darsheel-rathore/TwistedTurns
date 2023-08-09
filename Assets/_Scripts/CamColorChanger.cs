using UnityEngine;

public class CamColorChanger : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.green;
    public float cycleDuration = 2.0f;

    private Camera mainCamera;
    private float lerpFactor = 0.0f;
    private float timer = 0.0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Calculate the interpolation factor based on the cycle duration
        lerpFactor = Mathf.PingPong(timer / cycleDuration, 1.0f);

        // Interpolate between startColor and endColor based on the lerpFactor
        Color lerpedColor = Color.Lerp(startColor, endColor, lerpFactor);

        // Set the interpolated color as the skybox color of the main camera
        mainCamera.backgroundColor = lerpedColor;
    }
}
