using UnityEngine;

public class FreezeRigidbodies : MonoBehaviour
{
    private bool isFrozen = false;
    [SerializeField] Rigidbody2D[] allRigidbodies;
    [SerializeField] float[] originalGravityScales;

    private void Start()
    {
        // Find all active Rigidbody2D components in the scene
        //allRigidbodies = FindObjectsOfType<Rigidbody2D>();

        // Store the original gravity scales of all Rigidbody2D objects
        originalGravityScales = new float[allRigidbodies.Length];
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            originalGravityScales[i] = allRigidbodies[i].gravityScale;
        }
    }

    public void ToggleFreezeRigidbodies()
    {
        isFrozen = !isFrozen;

        // Loop through each Rigidbody2D and adjust properties based on freeze state
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            Rigidbody2D rb = allRigidbodies[i];
            if (isFrozen)
            {
                // Freeze the object by setting gravity scale to 0 and velocity to 0
                rb.gravityScale = 0;
                //rb.velocity = Vector2.zero;
            }
            else
            {
                // Restore the original gravity scale
                rb.gravityScale = originalGravityScales[i];
            }
        }
    }
}
