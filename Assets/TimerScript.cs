using UnityEngine;
using TMPro;
using UnityEditor;

public class TimerScript : MonoBehaviour
{
   [SerializeField] private int DefaultTimerDuration = 90;
   [SerializeField] private int timerDuration ; // Duration of each timer cycle in seconds
   [SerializeField] private int timer; // Current timer value

    [SerializeField]
    private string timeString; // Timer value as a string

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerText2;
    public static TimerScript Instance { get; private set; }

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
        PlayerPrefs.SetInt("LastHPTimer", timer);
        PlayerPrefs.Save();
    }


    public void RefreshTimer()
    {
        //Debug.Log("refreshed timer");

        if (PlayerPrefs.GetInt("hp") == 3)
        {
            if (timerText != null)
            {
                timerText.enabled = false;
            }
            //reset timer to max so it isnt helping players in the background
            timer = DefaultTimerDuration;
            PlayerPrefs.SetInt("LastHPTimer", timer);
        }
        else
        {
            if(timerText != null)
            {
                timerText.enabled = true;
                InvokeRepeating("UpdateTimer", 0f, 1f);
            }
        }
    }

    private void UpdateTimer()
    {
        if (timerText.gameObject.GetComponent<TextMeshProUGUI>().enabled == true)
        {

            timer--;

            if (timer <= 0)
            {
                Debug.Log("Hello jello");


               timer = DefaultTimerDuration;


                if(PlayerPrefs.GetInt("hp") < 3)
                {
                    timerText.enabled = true;
                    GameManager.Instance.HealPlayer(1);
                    PlayerPrefs.SetInt("hp", GameManager.Instance.curHealth);
                }
                else
                {
                    timerText.enabled = false;
                }
            }


        }


        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timeString = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");

        timerText2.text = "(You get 1 life every " + minutes.ToString("D2") + "m" + seconds.ToString("D2") + "s" + ")";


        //Debug.Log("Timer: " + timeString);
    }
}
