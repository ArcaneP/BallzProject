using UnityEngine;

public class VelocityNormalizer : MonoBehaviour
{
    public float maxVelocity = 3f; // Maximum allowed velocity
    public bool isLogging = false; // Enable/disable logging

    public Rigidbody2D rb;

    public float maxLoggedVelocity = 0f; // Store the maximum logged velocity

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Log current velocity if logging is enabled
        if (isLogging)
        {
            Debug.Log($"Current velocity: {rb.velocity.magnitude}");
            if (rb.velocity.magnitude > maxLoggedVelocity)
            {
                maxLoggedVelocity = rb.velocity.magnitude;
            }
        }

        // Normalize velocity if not logging and it exceeds the maximum allowed velocity
        if (!isLogging && rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    // Call this method to reset the maximum logged velocity
    public void ResetMaxLoggedVelocity()
    {
        maxLoggedVelocity = 0f;
    }
}
