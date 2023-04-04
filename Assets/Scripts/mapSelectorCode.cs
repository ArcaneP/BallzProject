using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mapSelectorCode : MonoBehaviour
{
    public LevelList levelList;
    public int levelPointer = 0;

    public GameObject iconOBJ;

    public GameObject buttonLeft, buttonRight;

    private void Start()
    {
        levelPointer = 0;
        iconOBJ.GetComponent<Image>().sprite = levelList.levels[levelPointer];

        Debug.Log("levellist is: " + levelList.levels.Length);

        if(levelPointer == 0)
        {
            buttonLeft.gameObject.SetActive(false);
        }
    }

    public void rightButton()
    {

        if (levelPointer < levelList.levels.Length - 1)
        {
            levelPointer++;
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

}
