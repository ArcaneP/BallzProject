using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

    

    public static GameManager Instance { get; private set; }

    [SerializeField] private string curSceneName;
    [SerializeField] private int number;
    
    void Start()
    {
        
        Application.targetFrameRate = -1;
    }

    private void Awake()
    {   

        if (PlayerPrefs.GetInt("hp") > maxHealth)
        {
            curHealth = maxHealth;
            PlayerPrefs.SetInt("hp", curHealth);
        }


        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        SelectHeartImages();

        
        if(PlayerPrefs.GetInt("hp") > 0)
        {
            curHealth = PlayerPrefs.GetInt("hp");
        }

    }


    public void ChangeLevelIndex()
    {
        curSceneName = SceneManager.GetActiveScene().name;
        number = int.Parse(Regex.Match(curSceneName, @"\d+").Value);

        PlayerPrefs.SetInt("lastSceneName", number);

    }

    public void ChangeNextLevelIndex()
    {
        curSceneName = SceneManager.GetActiveScene().name;
        number = int.Parse(Regex.Match(curSceneName, @"\d+").Value);

        PlayerPrefs.SetInt("lastSceneName", number +1);

    }

    public void ChangeLevelIdx(int num)
    {
        PlayerPrefs.SetInt("lastSceneName", num);
    }

    void SelectHeartImages()
    {
        // Assign the heart images to the corresponding array elements
        hearts = new Image[3];

        if(heart1 == null) { heart1 = GameObject.FindGameObjectWithTag("h1").GetComponent<Image>(); hearts[0] = heart1; }

        if (heart2 == null) { heart2 = GameObject.FindGameObjectWithTag("h2").GetComponent<Image>(); hearts[1] = heart2; }

        if (heart3 == null) { heart3 = GameObject.FindGameObjectWithTag("h3").GetComponent<Image>(); hearts[2] = heart3; }

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

    public void LoadLastLevel()
    {
        if(PlayerPrefs.GetInt("lastSceneName") != 0)
        {
            // uncomment to fix if bugs
            Debug.Log("level " + PlayerPrefs.GetInt("lastSceneName"));
            SceneManager.LoadScene("level " + PlayerPrefs.GetInt("lastSceneName"));


            //TRY TO MAKE IT LOAD USING COOL LOAD BAR

            /*SceneManager.LoadScene("LoadingScene");
            LoadingBarScript.Instance.destSceneName = "level " + PlayerPrefs.GetInt("lastSceneName");
            Debug.Log("loading now: " + LoadingBarScript.Instance.destSceneName);*/



        }
        else
        {
            SceneManager.LoadScene("level 1");
            PlayerPrefs.SetInt("lastSceneName", 1);
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

    public void HealPlayer(int health)
    {
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        else
        {
            curHealth += health;
        }
    }

    public void TryAgainButton()
    {
        AdsInitializer.Instance.LoadInerstitialAd();

        PlayerPrefs.SetInt("hp", 1);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    void GameOver()
    {
        ClearBalls();
        losescreen.SetActive(true);
        //Time.timeScale = 0;
        //set back tp to max
        //PlayerPrefs.SetInt("hp", 0);
    }

    public void Win()
    {
        ClearBalls();

        winscreen.SetActive(true);
        losescreen.SetActive(false);
        //Time.timeScale = 0;

    }

    private void ClearBalls()
    {
        // Find all game objects with the name "Circle(Clone)"
        GameObject[] balls = GameObject.FindObjectsOfType<GameObject>().Where(go => go.name == "Circle(Clone)").ToArray();

        Debug.Log("balls count: " + balls.Length);

        // Destroy all found game objects
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }

    public void ClearGarbage()
    {
        System.GC.Collect();
    }
}
