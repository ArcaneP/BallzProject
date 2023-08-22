using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int curHealth, maxHealth = 3;

    public GameObject winscreen, losescreen;

    public Image[] hearts;

    private Image heart1;
    private Image heart2;
    private Image heart3;

    [SerializeField] Button ExitToLevelsBttn;

    public static GameManager Instance { get; private set; }

    [SerializeField] private string curSceneName;
    [SerializeField] private int number;

    [SerializeField] bool isFrenzyMode = false;
    [SerializeField] int frenzyScore;

    [SerializeField] TextMeshProUGUI frenzyScoreText;

    void Start()
    {
        Application.targetFrameRate = 90;
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isFrenzy") == 0)
        {
            isFrenzyMode = false;
        }
        else
        {
            isFrenzyMode = true;
            frenzyScore = PlayerPrefs.GetInt("frenzyScore");
        }

        if (SceneManager.GetActiveScene().name == "menu")
        {
            if (PlayerPrefs.GetInt("savedFrenzyS") > 0)
            {
                frenzyScoreText.text = PlayerPrefs.GetInt("savedFrenzyS").ToString();
            }
            else
            {
                frenzyScoreText.text = "-";
            }
        }
        else //other than the menu load the score in levels pretty much
        {
            if (isFrenzyMode)
            {

                if (PlayerPrefs.GetInt("frenzyScore") > 0)
                {
                    frenzyScoreText.text = PlayerPrefs.GetInt("frenzyScore").ToString();
                }
                else
                {
                    frenzyScoreText.text = "0";
                }
            }
        }


        if (SceneManager.GetActiveScene().name != "demo")
        {
            //BLOCK HEALTH FROM BEING MORE THAN MAX UNLESS TUTORIAL 
            if (PlayerPrefs.GetInt("hp") > maxHealth)
            {
                curHealth = maxHealth;
                PlayerPrefs.SetInt("hp", curHealth);
            }
        }



        if (PlayerPrefs.GetInt("hp") > 0)
        {
            curHealth = PlayerPrefs.GetInt("hp");
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

        if (curHealth < 3)
        {
            //TimerScript.Instance.timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            TimerScript.Instance.enabledTimer = true;
        }
        else
        {
            TimerScript.Instance.timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        }


        if (ExitToLevelsBttn != null)
        {
            ExitToLevelsBttn.onClick.AddListener(TakeDmg);
            ExitToLevelsBttn.onClick.AddListener(LoadLevelMenu);
        }


    }

    //private void FixedUpdate()
    //{
    //    if(losescreen != null)
    //    {
    //        if(curHealth > 0) { losescreen.SetActive(false); }
    //    }
    //}

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

        PlayerPrefs.SetInt("lastSceneName", number + 1);

    }

    public void ChangeLevelIdx(int num)
    {
        PlayerPrefs.SetInt("lastSceneName", num);
    }

    void SelectHeartImages()
    {
        // Assign the heart images to the corresponding array elements
        hearts = new Image[3];

        if (heart1 == null) { heart1 = GameObject.FindGameObjectWithTag("h1").GetComponent<Image>(); hearts[0] = heart1; }

        if (heart2 == null) { heart2 = GameObject.FindGameObjectWithTag("h2").GetComponent<Image>(); hearts[1] = heart2; }

        if (heart3 == null) { heart3 = GameObject.FindGameObjectWithTag("h3").GetComponent<Image>(); hearts[2] = heart3; }

    }


    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < curHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }

        if (curHealth == 0 && losescreen != null)
        {
            losescreen.SetActive(true);
            //GameOver();
        }
    }

    public void LoadLastLevel()
    {
        if (curHealth > 0)
        {

            if (PlayerPrefs.GetInt("lastSceneName") != 0)
            {
                PlayerPrefs.SetInt("isFrenzy", 0);
                isFrenzyMode = false;
                Debug.Log("FrenzyMode Disabled");
                PlayerPrefs.SetInt("frenzyScore", 0);

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
        else { AdsInitializer.Instance.ShowNoHealthUI(); }
    }

    private void TakeDmg()
    {
        curHealth -= 1;
        PlayerPrefs.SetInt("hp", curHealth);

        if (curHealth < 3)
        {
            TimerScript.Instance.ResetTimer();
            TimerScript.Instance.enabledTimer = true;
            TimerScript.Instance.timerText.enabled = true;
        }
    }

    public void TakeDamage(int damage)
    {

        if (curHealth > 0)
        {

            SoundManager.Instance.PlayLoseHPSFX();
            curHealth -= damage;
            PlayerPrefs.SetInt("hp", curHealth);

            if (PlayerPrefs.GetInt("hp") > 0)
            {

                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);

                TimerScript.Instance.ResetTimer();
                TimerScript.Instance.enabledTimer = true;
                TimerScript.Instance.timerText.enabled = true;
            }
        }

        if (curHealth <= 0 && PlayerPrefs.GetInt("hp") <= 0)
        {
            GameOver();
        }


    }

    public void HealPlayer(int health)
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
            PlayerPrefs.SetInt("hp", GameManager.Instance.curHealth);
        }
        else
        {
            curHealth += health;
            PlayerPrefs.SetInt("hp", GameManager.Instance.curHealth);
            TimerScript.Instance.RefreshTimer();
        }
    }

    public void TryAgainButton()
    {
        AdsInitializer.Instance.LoadRewardedAd();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    void GameOver()
    {
        if (!FinishLine.Instance.isDone)
        {
            Debug.Log("Say Hola once");
            SoundManager.Instance.PlayDefeatSFX();
            ClearBalls();
            losescreen.SetActive(true);
            if (isFrenzyMode) { PlayerPrefs.SetInt("frenzyScore", frenzyScore); } else { frenzyScoreText.enabled = false; }

        }
    }

    public void PlayFrenzy()
    {
        if (curHealth > 0)
        {
            Debug.Log("FRENZY MODEE!!! ");
            PlayerPrefs.SetInt("isFrenzy", 1);
            PlayerPrefs.SetInt("frenzyScore", 0);
            SceneManager.LoadScene("level 1");

        }
        else { AdsInitializer.Instance.ShowNoHealthUI(); }

    }

    public void Win()
    {
        Debug.Log("Say Hi once");
        if (isFrenzyMode)
        {
            frenzyScore++;
            Debug.Log("fernzyscore: " + frenzyScore);
            if (isFrenzyMode) { PlayerPrefs.SetInt("frenzyScore", frenzyScore); }
        }

        SoundManager.Instance.PlayerWinSFX();
        ClearBalls();
        winscreen.SetActive(true);
        losescreen.SetActive(false);

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

    public void LoadScene(string sceneName)
    {
        if (curHealth > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        else { AdsInitializer.Instance.ShowNoHealthUI(); }
    }

    private void LoadLevelMenu()
    {
        if (isFrenzyMode)
        {
            if (PlayerPrefs.GetInt("frenzyScore") > PlayerPrefs.GetInt("savedFrenzyS"))
            {
                PlayerPrefs.SetInt("savedFrenzyS", frenzyScore);
            }
            PlayerPrefs.SetInt("frenzyScore", 0);
        }

        SceneManager.LoadScene("levels");
    }

    public void SimpleLoadScene(string sceneName)
    {

        if (isFrenzyMode)
        {
            if (PlayerPrefs.GetInt("frenzyScore") > PlayerPrefs.GetInt("savedFrenzyS"))
            {
                PlayerPrefs.SetInt("savedFrenzyS", frenzyScore);
            }
            PlayerPrefs.SetInt("frenzyScore", 0);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void CompleteTutorial() 
    {
        TutorialManagerScript.Instance.CompletedTutorial();
    }
}
