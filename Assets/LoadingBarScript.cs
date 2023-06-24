using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingBarScript : MonoBehaviour
{
    public string destSceneName; // The name of your desination scene to get to

    [SerializeField] Slider loadingSlider;

    public bool simulateSlowLoading = false; // Flag to simulate slow loading

    public static LoadingBarScript Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(LoadDestinationSceneAsync());
    }

    IEnumerator LoadDestinationSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(destSceneName);

        // Don't activate the scene immediately
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Update your loading UI with the loading progress
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Divide by 0.9f to get 0 to 1 range
            UpdateLoadingUI(progress);

            // If the loading is almost complete, activate the scene
            if (progress >= 1.0f)
            {
                if (simulateSlowLoading)
                {
                    //Application.targetFrameRate = 10;

                    loadingSlider.value = 50;
                    yield return new WaitForSeconds(5f); // Simulate a 5-second delay
                }

                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    void UpdateLoadingUI(float progress)
    {
        loadingSlider.value = progress;

        // Update your loading UI elements (e.g., progress bar) with the progress value
    }

}
