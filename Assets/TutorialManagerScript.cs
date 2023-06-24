using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerScript : MonoBehaviour
{
    [SerializeField] GameObject PlayButton,LevelButton, OptionsButton, DemoButton, tutButton;

    [SerializeField] bool hasCompletedTutorial;


    [SerializeField] GameObject hp, video, howto;

    [SerializeField] int currentStep = 0;
    bool step1Completed = false;
    bool step2Completed = false;
    bool step3Completed = false;

    private void Start()
    {      
        if(PlayerPrefs.GetFloat("hasCompletedTutorial") == 0)
        {
            hasCompletedTutorial = false;
        }
        else
        {
            hasCompletedTutorial = true;
        }

        if (!hasCompletedTutorial)
        {

            StartTutorial();
            NextStep();
        }
        else
        {

            DemoButton.SetActive(false);

            this.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            Debug.Log("Tutorial has already been complete");

            
        }
    }

    public void StartTutorial()
    {
        GameManager.Instance.curHealth = 2;

        // Show initial instructions/UI elements
        // Activate necessary game objects
        // Set up any initial conditions

        if(PlayButton != null)
        {
            PlayButton.SetActive(false);
        }

        if(LevelButton != null)
        {
            LevelButton.SetActive(false);
        }

        if (OptionsButton != null)
        {
            OptionsButton.SetActive(false);
        }

        if(DemoButton != null)
        {
            DemoButton.SetActive(false);
        }


    }

    // Call this function to proceed to the next step
    public void NextStep()
    {
        currentStep++;

        // Check the current step and perform actions accordingly
        switch (currentStep)
        {
            case 1:
                if (!step1Completed)
                {
                    // Execute Step 1 logic
                    // Show instructions for Step 1
                    hp.SetActive(true);
                    Debug.Log("step1 Completed");
                }
                else
                {
                    // Step 1 already completed, move to the next step
                    NextStep();
                }
                break;
            case 2:
                if (!step2Completed)
                {
                    // Execute Step 2 logic
                    // Show instructions for Step 2
                    hp.SetActive(false);
                    video.SetActive(true);
                    Debug.Log("step2 Completed");
                }
                else
                {
                    // Step 2 already completed, move to the next step
                    NextStep();
                }
                break;
            case 3:
                if (!step3Completed)
                {
                    // Execute Step 3 logic
                    // Show instructions for Step 3
                    video.SetActive(false);
                    tutButton.SetActive(false);

                    howto.SetActive(true);
                    if(DemoButton != null)
                    {
                        DemoButton.SetActive(true);
                    }

                    Debug.Log("step3 Completed");
                }
                else
                {

                }
                break;

            default:

                break;
        }
    }

    // Call this function when a step is completed
    public void CompleteStep(int step)
    {
        switch (step)
        {
            case 1:
                step1Completed = true;
                Debug.Log("step1 Completed");
                break;
            case 2:
                step2Completed = true;
                Debug.Log("step2 Completed");
                break;
            case 3:
                step3Completed = true;
                Debug.Log("step3 Completed");
                break;
            // Add cases for additional steps
            default:
                break;
        }

        // Move to the next step
        NextStep();
    }

    public void CompletedTutorial()
    {
        hasCompletedTutorial = true;
        PlayerPrefs.SetFloat("hasCompletedTutorial", 1);  
    }

    public void ReDoTutorial() 
    {
        hasCompletedTutorial = false;
        PlayerPrefs.SetFloat("hasCompletedTutorial", 0);

        Debug.Log("can redo tutorial now");
    }


}
