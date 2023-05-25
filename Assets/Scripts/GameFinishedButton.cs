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
        curSceneName = SceneManager.GetActiveScene().name;
        number = int.Parse(Regex.Match(curSceneName, @"\d+").Value);
    }


    public void NextButton()
    {
        SceneManager.LoadScene("level "+ (number + 1));
    }
}
