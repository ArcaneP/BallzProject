using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int curHealth, maxHealth = 3;

    public GameObject winscreen, losescreen;

    public Image[] hearts;

    private Image heart1;
    private Image heart2;
    private Image heart3;

    [SerializeField] private GameObject showADbutton; //only show when health is bellow max

    public static GameManager Instance { get; private set; }

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

    private void Start()
    {
        SelectHeartImages();

        Application.targetFrameRate = 100;
        if(PlayerPrefs.GetInt("hp") > 0)
        {
            curHealth = PlayerPrefs.GetInt("hp");
        }

        ShowADButton();
    }

    void SelectHeartImages()
    {
        heart1 = GameObject.FindGameObjectWithTag("h1").GetComponent<Image>();
        heart2 = GameObject.FindGameObjectWithTag("h2").GetComponent<Image>();
        heart3 = GameObject.FindGameObjectWithTag("h3").GetComponent<Image>();

        // Assign the heart images to the corresponding array elements
        hearts = new Image[3];
        hearts[0] = heart1;
        hearts[1] = heart2;
        hearts[2] = heart3;
    }

    void ShowADButton()
    {
        if(curHealth < maxHealth)
        {
            showADbutton.SetActive(true);
        }
        else
        {
            showADbutton.SetActive(false);
        }
    }

    private void Update()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < curHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        } 
    }

    public void TakeDamage(int damage)
    {       

        if(curHealth > 0)
        {

            curHealth -= damage;
            PlayerPrefs.SetInt("hp", curHealth);

            //EditorApplication.isPaused = true;

            if (PlayerPrefs.GetInt("hp") > 0)
            {
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }
        }

        if (curHealth <= 0 && PlayerPrefs.GetInt("hp") <= 0)
        {
            GameOver();
        }


    }

    public void TryAgainButton()
    {
        PlayerPrefs.SetInt("hp", maxHealth);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1; 
    }

    void GameOver()
    {
        losescreen.SetActive(true);
        //Time.timeScale = 0;
        //set back tp to max
        //PlayerPrefs.SetInt("hp", 0);
    }

    public void Win()
    {
        winscreen.SetActive(true);
        Time.timeScale = 0;

    }
}
