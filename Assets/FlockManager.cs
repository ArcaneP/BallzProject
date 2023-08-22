using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject circlePrefab;
    public int circleCount = 4;
    public float spawnRadius = 5f;
    public Vector3 customSpawnPosition; // Specify the spawn position in the Inspector

    private void OnDrawGizmos()
    {
        // Visualize the custom spawn position with a wire sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(customSpawnPosition, spawnRadius);
    }

    private void Start()
    {
        for (int i = 0; i < circleCount; i++)
        {
            Vector2 spawnPosition = (Vector2)customSpawnPosition + Random.insideUnitCircle * spawnRadius;
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D circleRb = circle.GetComponent<Rigidbody2D>();
            if (circleRb != null)
            {
                //circleRb.gravityScale = 0f; // Set gravity scale to zero
            }
            else
            {
                Debug.LogWarning("Rigidbody2D component not found on the spawned circle.");
            }

            circle.GetComponent<FlockBehavior>().moveSpeed = Random.Range(2f, 5f);
        }
    }
}
