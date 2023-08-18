using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerScript : MonoBehaviour
{
    [SerializeField] GameObject PlayButton, LevelButton, OptionsButton, DemoButton, tutButton, FrenzyModeBttn;
    [SerializeField] bool hasCompletedTutorial;
    [SerializeField] GameObject hp, video, howto;
    [SerializeField] int currentStep = 0;
    bool[] stepCompleted = new bool[4];

    private void Start()
    {
        hasCompletedTutorial = PlayerPrefs.GetFloat("hasCompletedTutorial") != 0;

        if (!hasCompletedTutorial)
        {
            StartTutorial();
            NextStep();
        }
        else
        {
            DemoButton.SetActive(false);
            GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            Debug.Log("Tutorial has already been completed");
        }
    }

    public void StartTutorial()
    {
        PlayerPrefs.SetInt("isFrenzy", 0);
        PlayerPrefs.SetInt("lastSceneName", 1);
        GameManager.Instance.curHealth = 999;
        PlayerPrefs.SetInt("hp", 999);
        //GameManager.Instance.HealPlayer(3);
        TimerScript.Instance.enabledTimer = false;

        FrenzyModeBttn.SetActive(false);
        PlayButton.SetActive(false);
        LevelButton.SetActive(false);
        OptionsButton.SetActive(false);
        DemoButton.SetActive(false);
    }

    public void NextStep()
    {
        if (currentStep < stepCompleted.Length)
        {
            currentStep++;

            while (currentStep <= stepCompleted.Length && stepCompleted[currentStep - 1])
            {
                currentStep++;
            }

            if (currentStep <= stepCompleted.Length)
            {
                switch (currentStep)
                {
                    case 1:
                        hp.SetActive(true);
                        Debug.Log("step1 Completed");
                        break;
                    case 2:
                        hp.SetActive(false);
                        video.SetActive(true);
                        Debug.Log("step2 Completed");
                        break;
                    case 3:
                        video.SetActive(false);
                        tutButton.SetActive(false);
                        howto.SetActive(true);
                        Invoke("DisableHowTo", 1.5f);
                        Debug.Log("step3 Completed");
                        DemoButton.SetActive(true);
                        break;
                    //case 4:
                    //    howto.SetActive(false);
                    //    video.SetActive(false);
                    //    tutButton.SetActive(false);
                    //    Debug.Log("step4 Completed");
                    //    break;
                    default:
                        break;
                }
            }
        }
    }

    private void DisableHowTo()
    {
        howto.GetComponent<Image>().enabled = false;
    }

    public void CompleteStep(int step)
    {
        if (step >= 1 && step <= stepCompleted.Length)
        {
            stepCompleted[step - 1] = true;
            Debug.Log($"step{step} Completed");
        }

        NextStep();
    }

    public void CompletedTutorial()
    {
        hasCompletedTutorial = true;
        PlayerPrefs.SetFloat("hasCompletedTutorial", 1);
        TimerScript.Instance.enabledTimer = false;
    }

    public void ReDoTutorial()
    {
        hasCompletedTutorial = false;
        PlayerPrefs.SetFloat("hasCompletedTutorial", 0);
        Debug.Log("can redo tutorial now");
    }
}
