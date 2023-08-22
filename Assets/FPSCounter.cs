using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FPSCounter : MonoBehaviour
{
    private Canvas canvas;
    private TextMeshProUGUI text;
    private Dictionary<int, string> cachedNumberStrings = new Dictionary<int, string>();
    private int[] frameRateSamples;
    private int cacheNumbersAmount = 300;
    private int averageFromAmount = 30;
    private int averageCounter = 0;
    private int currentAveraged;

    void Awake()
    {
        CreateCanvasAndText();

        // Cache strings and create array
        for (int i = 0; i < cacheNumbersAmount; i++)
        {
            cachedNumberStrings[i] = i.ToString();
        }
        frameRateSamples = new int[averageFromAmount];
    }

    void Update()
    {
        // Sample
        var currentFrame = (int)Mathf.Round(1f / Time.smoothDeltaTime); // If your game modifies Time.timeScale, use unscaledDeltaTime and smooth manually (or not).
        frameRateSamples[averageCounter] = currentFrame;

        // Average
        var average = 0f;

        foreach (var frameRate in frameRateSamples)
        {
            average += frameRate;
        }

        currentAveraged = (int)Mathf.Round(average / averageFromAmount);
        averageCounter = (averageCounter + 1) % averageFromAmount;

        // Assign to UI and change color
        text.text = currentAveraged switch
        {
            var x when x < cacheNumbersAmount && x > 0 => cachedNumberStrings[x],
            var x when x < 0 => "< 0",
            var x when x > cacheNumbersAmount => $"> {cacheNumbersAmount}"
        };

        // Change color based on FPS range
        if (currentAveraged < 30)
        {
            text.color = Color.red;    // Low FPS, red color
        }
        else if (currentAveraged < 60)
        {
            text.color = Color.yellow; // Medium FPS, yellow color
        }
        else
        {
            text.color = Color.green;  // High FPS, green color
        }
    }

    private void CreateCanvasAndText()
    {
        // Create a new Canvas
        canvas = new GameObject("FPS Canvas", typeof(Canvas)).GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = int.MaxValue; // Place it on top of everything

        // Create a TextMeshPro Text
        text = new GameObject("FPS Text", typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.transform.SetParent(canvas.transform);
        text.rectTransform.anchorMin = new Vector2(0, 1); // Top-left corner
        text.rectTransform.anchorMax = new Vector2(0, 1);
        text.rectTransform.pivot = new Vector2(0, 1);
        text.rectTransform.anchoredPosition = new Vector2(10, -10); // Offset from corner
        text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Arial SDF");
        text.fontSize = 100;
        text.color = Color.white;
    }
}
