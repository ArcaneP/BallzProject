using UnityEngine;
using TMPro;

public class HealthOnTimer : MonoBehaviour
{
    private const string LastExecutionTimeKey = "LastExecutionTime";
    private float interval = 1800f; // 30 minutes in seconds

    public TextMeshProUGUI timeLeftText;

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Store the current time when the app is paused
            PlayerPrefs.SetFloat(LastExecutionTimeKey, Time.realtimeSinceStartup);
            PlayerPrefs.Save();
        }
        else
        {
            // Check if the desired interval has passed since the app was paused
            float lastExecutionTime = PlayerPrefs.GetFloat(LastExecutionTimeKey, 0f);
            float elapsedTime = Time.realtimeSinceStartup - lastExecutionTime;
            float remainingTime = interval - elapsedTime;

            if (remainingTime <= 0f)
            {
                // Perform the desired action
                Debug.Log("Health added");

                // Update the last execution time to the current time
                PlayerPrefs.SetFloat(LastExecutionTimeKey, Time.realtimeSinceStartup);
                PlayerPrefs.Save();

                remainingTime = interval;
            }

            // Update the text component with the remaining time
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            if (timeLeftText != null)
            {
                timeLeftText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
