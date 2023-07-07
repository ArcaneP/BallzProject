using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHatcg : MonoBehaviour
{
    public GameObject hatch;
    [SerializeField] int asd = 1;
    private Animator anim;

    private void Start()
    {
        
        //hatch = GameObject.FindGameObjectWithTag("Holder");
        //anim = GameObject.FindObjectOfType<Animator>();
        

        hatch = GameObject.FindGameObjectWithTag("Holder");
        anim = hatch.gameObject.GetComponent<Animator>();
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
