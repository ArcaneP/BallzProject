using UnityEditorInternal;
using UnityEngine;

public class PushOnIdle : MonoBehaviour
{
    public float idleTime = 5f;
    public float pushForce = 1f;

    private Rigidbody2D rb;
    private float idleTimer;
    private bool isIdle;

    private void Start()
    {
        pushForce = Random.RandomRange(50f, 100f);

        rb = GetComponent<Rigidbody2D>();
        idleTimer = 0f;
        isIdle = false;
    }

    private void Update()
    {
        if (rb.velocity.magnitude <= 0.01f)
        {
            if (!isIdle)
            {
                idleTimer = 0f;
                isIdle = true;
            }
            else
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleTime)
                {
                    PushRandomDirection();
                    idleTimer = 0f;
                    isIdle = false;
                }
            }
        }
        else
        {
            idleTimer = 0f;
            isIdle = false;
        }
    }

    private void PushRandomDirection()
    {
        Debug.Log("pushed in random direction", gameObject);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDirection * pushForce, ForceMode2D.Impulse);
        rb.gravityScale = -rb.gravityScale;
    }
}
