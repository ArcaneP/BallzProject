using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mapSelectorCode : MonoBehaviour
{
    public LevelList levelList;
    public int levelPointer = 0;

    public GameObject iconOBJ;

    public GameObject buttonLeft, buttonRight;

    public TextMeshProUGUI UIText;

    private void Start()
    {

        if(PlayerPrefs.GetInt("lastSceneName") == 0)
        {
            levelPointer = 0;
        }
        else
        {
         levelPointer = (PlayerPrefs.GetInt("lastSceneName") - 1);
        }



        iconOBJ.GetComponent<Image>().sprite = levelList.levels[levelPointer];

        Debug.Log("levellist is: " + levelList.levels.Length);

        if(levelPointer <= 1)
        {
            buttonLeft.gameObject.SetActive(false);
        }else
        {
            buttonLeft.gameObject.SetActive(true);
        }

        if (levelPointer == levelList.levels.Length - 1)
        {
            buttonRight.gameObject.SetActive(false);
        }
        else
        {
            buttonRight.gameObject.SetActive(true);
        }

        UIText.SetText("LEVEL: " + PlayerPrefs.GetInt("lastSceneName"));
    }

    public void rightButton()
    {

        if (levelPointer < levelList.levels.Length - 1)
        {
            levelPointer++;
            UIText.SetText("LEVEL: " + (levelPointer + 1) );
            Debug.Log(levelPointer);
            iconOBJ.GetComponent<Image>().sprite = levelList.levels[levelPointer];
        }

        if (levelPointer > 0)
        {
            buttonLeft.gameObject.SetActive(true);
        }

        if(levelPointer == levelList.levels.Length - 1)
        {
            buttonRight.gameObject.SetActive(false);
        }

    }
    public void leftButton()
    {
        if (levelPointer < levelList.levels.Length - 1 || levelPointer < levelList.levels.Length)
        {
            if(levelPointer >= 0)
            {
                levelPointer--;
                UIText.SetText("LEVEL: " + (levelPointer + 1) );
            }
            Debug.Log(levelPointer);
            iconOBJ.GetComponent<Image>().sprite = levelList.levels[levelPointer];
        }

        if (levelPointer == 0)
        {
            buttonLeft.gameObject.SetActive(false);
        }

        if (levelPointer < levelList.levels.Length - 1)
        {
            buttonRight.gameObject.SetActive(true);
        }

    }

    public void LoadMap()
    {
        SceneManager.LoadScene("level "+ (levelPointer+1));
        GameManager.Instance.ChangeLevelIdx(levelPointer + 1);
    }

}
