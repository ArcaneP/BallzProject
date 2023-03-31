using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mapSelectorCode : MonoBehaviour
{
    public LevelList levelList;
    public int levelPointer = 0;

    public GameObject iconOBJ;

    private void Awake()
    {
        levelPointer = 0;
        iconOBJ.GetComponent<Image>().sprite = levelList.levels[levelPointer];

    }

    public void rightButton()
    {
        if (levelPointer < levelList.levels.Length - 1)
        {         
            levelPointer++;
            Debug.Log(levelPointer);
            iconOBJ.GetComponent<Image>().sprite = levelList.levels[levelPointer];
        }

        if(levelPointer == levelList.levels.Length - 1)
        {
            levelPointer = 0;
        }
    }
}
