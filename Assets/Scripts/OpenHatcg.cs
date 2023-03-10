using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHatcg : MonoBehaviour
{
    public GameObject hatch;
    int asd = 0;

    public void Open()
    {

        if(asd == 0)
        {
            hatch.SetActive(false);
            asd = 1;
        }
        else
        {
            hatch.SetActive(true);
            asd = 0;
        }
    }
}
