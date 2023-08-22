using System.Collections.Generic;
using UnityEngine;

public class FlockBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float neighborRadius = 2f;
    public float separationDistance = 1f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 cohesion = Vector2.zero;
        Vector2 alignment = Vector2.zero;
        Vector2 separation = Vector2.zero;

        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, neighborRadius);

        foreach (Collider2D neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject && neighbor.gameObject.GetComponent<FlockBehavior>())
            {
                Vector2 neighborDirection = neighbor.transform.position - transform.position;
                float distance = neighborDirection.magnitude;

                // Cohesion: Move towards the center of neighbors
                cohesion += (Vector2)neighbor.transform.position;

                // Alignment: Adjust heading based on neighbors' headings
                alignment += neighborDirection.normalized;

                // Separation: Move away from neighbors that are too close
                if (distance < separationDistance)
                {
                    separation -= neighborDirection.normalized / distance;
                }
            }
        }

        // Apply the calculated behaviors
        cohesion /= neighbors.Length;
        alignment /= neighbors.Length;
        separation /= neighbors.Length;

        Vector2 flockDirection = (cohesion + alignment + separation).normalized;
        rb.velocity = flockDirection * moveSpeed;
    }
}
