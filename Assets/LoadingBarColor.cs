using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarColor : MonoBehaviour
{

    public float cycleTime = 1.0f; // Time it takes to cycle through all colors
    public Color[] colors; // Array of colors to cycle through

    private Image img; // Reference to the SpriteRenderer component
    private float timer; // Timer used to cycle through colors
    private int colorIndex; // Index of the current color in the colors array
    private Color startColor; // Starting color for interpolation
    private Color endColor; // Ending color for interpolation

    void Start()
    {
        img = GetComponent<Image>();
        img.color = colors[colorIndex];
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
            startColor = img.color;
            endColor = colors[colorIndex];
        }

        float t = timer / cycleTime;
        img.color = Color.Lerp(startColor, endColor, t);
    }
}
