using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameFinishedButton : MonoBehaviour
{
    [SerializeField] private string curSceneName;
    [SerializeField] private int number;


    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "demo")
        {
        curSceneName = SceneManager.GetActiveScene().name;
        number = int.Parse(Regex.Match(curSceneName, @"\d+").Value);

        }
    }


    public void NextButton()
    {
        SceneManager.LoadScene("level "+ (number + 1));
        GameManager.Instance.ChangeNextLevelIndex(); //set playerprefab levelindex to +1 cause its the next level that should be saved to come back to later

        AdsInitializer.Instance.curTimesAdFree++;

        if(AdsInitializer.Instance.curTimesAdFree >= AdsInitializer.Instance.maxAdFreeMode)
        {
            AdsInitializer.Instance.LoadInerstitialAd();
        }

    }
}
