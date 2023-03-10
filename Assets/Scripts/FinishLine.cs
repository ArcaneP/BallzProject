using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public GameObject hatch;

    private GameManager gameMan;

    public TextMeshProUGUI curFillAmountText;

    public Image image;
    public int counter;
    public int endGoal;
    private int range = 4;

    private bool startcount;
    public float timer = 10;

    private float startTimer;

    private Animator anim;

    private void Start()
    {
        anim = hatch.GetComponent<Animator>();
        curFillAmountText.SetText(counter.ToString() + "/" + endGoal);
        gameMan = GameObject.FindObjectOfType<GameManager>();
        startTimer = timer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() && collision.gameObject.name != "x")
        {
            collision.gameObject.name = "x";
            counter++;

            curFillAmountText.SetText(counter.ToString() + "/" + endGoal);

        } 
    }

    IEnumerator waitTimer()
    {
        anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(3f);
    }

    private void Update()
    {
        //Debug.Log(image.fillAmount);
        image.fillAmount = -(-timer / startTimer);


        if (startcount)
        {

            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if(timer <= 0)
            {
                anim.SetBool("isOpen", true);

                if (counter >= endGoal - range && counter <= endGoal + range)
                {
                    gameMan.Win();
                }
                else
                {
                    gameMan.TakeDamage(1);
                }
            }
        }
    }

    public void StartCounting()
    {
        startcount = true;
    }
}
