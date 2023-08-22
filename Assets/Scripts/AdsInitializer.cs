using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    string _gameId;
    [SerializeField] bool _testMode = true;

    public static AdsInitializer Instance;

    [SerializeField] private GameObject showADbutton, getHealthUI; //only show when health is bellow max;

    public int maxAdFreeMode = 5;
    public int curTimesAdFree = 0;


    private void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.R))
        {
            GameManager.Instance.TakeDamage(1);
        }*/


        if (SceneManager.GetActiveScene().name == "menu")
        {
            ShowADButton();

            Time.timeScale = 1f;
        }
        else
        {
            showADbutton.SetActive(false);
        }

    }

    public void ShowNoHealthUI() 
    {
        getHealthUI.SetActive(true);
    }

    void ShowADButton()
    {
        if (showADbutton != null)
        {
            //Debug.Log("ShowADButton reference is not found :c");
            if (GameManager.Instance.curHealth < GameManager.Instance.maxHealth)
            {
                showADbutton.SetActive(true);
            }
            else
            {
                showADbutton.SetActive(false);
            }
        }
    }


        private void Awake()
    {

        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Make this object persistent across scenes


        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            //LoadRewardedAd(); //testing
            LoadInerstitialAd();
        }
        else
        {
            InitializeAds();
        }
    }


    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        //LoadInerstitialAd();
        LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInerstitialAd()
    {
        Advertisement.Load("Interstitial_Android", this);
    }

    public void LoadRewardedAd()
    {
        Debug.Log("LoadRewardedAd - mode: Rewarded_Android");
        Advertisement.Load("Rewarded_Android", this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded");
        Advertisement.Show(placementId, this); //not this

        AdsInitializer.Instance.curTimesAdFree = 0;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
        Time.timeScale = 0;
        Advertisement.Banner.Hide(); //why does it show a rewared ad every time
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            RewardPlayer();
        }

        if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.UNKNOWN.Equals(showCompletionState))
        {
            showADbutton.SetActive(false);
        }


        Time.timeScale = 1;
        Advertisement.Banner.Show("Banner_Android");
    }

    public void RewardPlayer()
    {
        Time.timeScale = 1;
        Debug.Log("rewarded Player");

        if(GameManager.Instance.curHealth < 3 && PlayerPrefs.GetInt("hp") < 3)
        {
            GameManager.Instance.HealPlayer(3);
            PlayerPrefs.SetInt("hp", GameManager.Instance.curHealth);

            if(GameManager.Instance.losescreen != null)
            {
                GameManager.Instance.losescreen.SetActive(false);   
            }

        }
        else
        {
            Debug.Log("max health reached");
        }

        if(GameManager.Instance.curHealth == 3)
        {
            TimerScript.Instance.timerText.enabled = false;
        }


    }

    public void LoadBannerAd()
    {
        Debug.Log("banner loaded succesfully");

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load("Banner_Android",
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }
            );
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show("Banner_Android");
    }

    void OnBannerError(string message)
    {
        Debug.Log("Banner Error:" + message);
    }

}