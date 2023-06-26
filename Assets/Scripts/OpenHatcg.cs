using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHatcg : MonoBehaviour
{
    public GameObject hatch;
    int asd = 1;
    private Animator anim;

    private void Start()
    {
        anim = GameObject.FindObjectOfType<Animator>();
    }

#if UNITY_EDITOR

    //pc space testing 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            FinishLine.Instance.StartCounting();
            Open();
        }
    }

#endif

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
