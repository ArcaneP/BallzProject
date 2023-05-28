using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // Singleton instance
    private static MusicManager instance;

    // Audio source to play the music
    public AudioSource audioSource;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Make this object persistent across scenes

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Play the music
        audioSource.Play();
    }


    // Play the music when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "menu") // Adjust the scene name as per your menu scene
        {
            audioSource.Play();
        }
    }

    // Play the music
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "menu") // Adjust the scene name as per your menu scene
        {
            audioSource.Play();
        }
    }

    // Unsubscribe from the event when the script is destroyed
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
