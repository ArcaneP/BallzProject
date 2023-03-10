using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int curHealth, maxHealth = 3;

    public GameObject winscreen, losescreen;

    public Image[] hearts;

    private void Start()
    {
        Application.targetFrameRate = 100;
        if(PlayerPrefs.GetInt("hp") > 0)
        {
            curHealth = PlayerPrefs.GetInt("hp");
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
    }
}
