using UnityEngine;

public class ColorCycle : MonoBehaviour
{

    public float cycleTime = 1.0f; // Time it takes to cycle through all colors
    public Color[] colors; // Array of colors to cycle through

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private float timer; // Timer used to cycle through colors
    private int colorIndex; // Index of the current color in the colors array
    private Color startColor; // Starting color for interpolation
    private Color endColor; // Ending color for interpolation

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[colorIndex];
        startColor = colors[colorIndex];
        endColor = colors[(colorIndex + 1) % colors.Length];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cycleTime)
        {
            timer -= cycleTime;
            colorIndex = (colorIndex + 1) % colors.Length;
            startColor = spriteRenderer.color;
            endColor = colors[colorIndex];
        }

        float t = timer / cycleTime;
        spriteRenderer.color = Color.Lerp(startColor, endColor, t);
    }
}