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
    private int range = 2;

    private bool startcount;
    [SerializeField] float timer = 6;

    private float startTimer;

    private Animator anim;

    public static FinishLine Instance;

    

    private void Awake()
    {
        timer = 6;

        curFillAmountText = GameObject.FindGameObjectWithTag("fillText").GetComponent<TextMeshProUGUI>();
        image = GameObject.FindGameObjectWithTag("fillicon").GetComponent<Image>();


        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        hatch = GameObject.FindGameObjectWithTag("Holder");
        anim = hatch.GetComponent<Animator>();
        curFillAmountText.SetText(counter.ToString() + "/" + endGoal);
        gameMan = GameObject.FindObjectOfType<GameManager>();
        startTimer = timer;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() && collision.gameObject.name != "x")
        {
            //collision.GetComponent<PushOnIdle>().enabled = false;
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

    private void FixedUpdate()
    {
        
        image.fillAmount = -(-timer / startTimer);


        if (startcount)
        {
           


            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if(timer <= 0)
            {
                anim.SetBool("isOpen", false);

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
