using UnityEngine;
using TMPro;
using UnityEditor;

public class TimerScript : MonoBehaviour
{
   [SerializeField] private int DefaultTimerDuration = 90;
   [SerializeField] private int timerDuration ; // Duration of each timer cycle in seconds
   [SerializeField] private float timer; // Current timer value

    [SerializeField]
    private string timeString; // Timer value as a string

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerText2;
    public static TimerScript Instance { get; private set; }

    public bool enabledTimer = true;

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
        if(PlayerPrefs.GetInt("LastHPTimer") != 0)
        {
            timerDuration = PlayerPrefs.GetInt("LastHPTimer");
        }
        else
        {
            timerDuration = DefaultTimerDuration;
        }

        timer = timerDuration;


        RefreshTimer();

    }

    public void SaveTime()
    {
        //Debug.Log("saving healthtimer for later");
        // Store the current time when the app is paused
        PlayerPrefs.SetInt("LastHPTimer", (int)timer);
        PlayerPrefs.Save();
    }


    public void RefreshTimer()
    {
        //Debug.Log("refreshed timer");

        if (PlayerPrefs.GetInt("hp") == 3)
        {
            if (timerText != null)
            {
                //timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            }
            //reset timer to max so it isnt helping players in the background
            timer = DefaultTimerDuration;
            PlayerPrefs.SetInt("LastHPTimer",(int)timer);
        }
        else
        {
            if(timerText != null)
            {
                timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                //InvokeRepeating("UpdateTimer", 0f, 1f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (enabledTimer)
        {
                PlayerPrefs.SetInt("LastHPTimer", (int)timer);

                if (timer <= 0)
                {
                    Debug.Log("Hello jello");


                    timer = DefaultTimerDuration;

                    //Timer hit zero heal player 1

                    if (PlayerPrefs.GetInt("hp") < 3)
                    {
                        timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                        enabledTimer = true;
                        GameManager.Instance.HealPlayer(1);
                        PlayerPrefs.SetInt("hp", GameManager.Instance.curHealth);
                        PlayerPrefs.Save();

                        if(PlayerPrefs.GetInt("hp") == 3)
                        {
                            enabledTimer = false;
                            timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                        }

                    }
                    else
                    {
                        enabledTimer = false;
                        timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                    }
                }
                else
                {

                    timer-= 1 * Time.fixedDeltaTime;
                    //timer -= 0.5f * Time.fixedDeltaTime;
                    //timer -= 60 * Time.fixedDeltaTime;

                    int minutes = Mathf.FloorToInt(timer / 60);
                    int seconds = Mathf.FloorToInt(timer % 60);
                    timeString = minutes.ToString("D2") + ":" + seconds.ToString("D2");
                    timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
                    timerText2.text = "(You get 1 life every " + minutes.ToString("D2") + "m" + seconds.ToString("D2") + "s" + ")";
                    //Debug.Log("Timer: " + timeString);
                }
           }
        }

    public void ResetTimer()
    {
        timer = DefaultTimerDuration;
        PlayerPrefs.SetInt("LastHPTimer", (int)timer);
    }

    /*public void UpdateTimer()
    {

    }*/

}