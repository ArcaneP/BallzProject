using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHatcg : MonoBehaviour
{
    public GameObject hatch;
    int asd = 0;
    private Animator anim;

    private void Start()
    {
        anim = GameObject.FindObjectOfType<Animator>();
    }

    public void Open()
    {

        if(asd == 0)
        {
            anim.SetBool("isOpen", false);
            asd = 1;
        }
        else
        {
            anim.SetBool("isOpen", true);
            asd = 0;
        }
    }
}
