using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FinishLine : MonoBehaviour
{
    public GameObject hatch;

    public TextMeshProUGUI curFillAmountText;

    public Image image;
    public int counter;
    public int endGoal;
   [SerializeField ]private int range = 2;

    private bool startcount;
   [SerializeField] float timer;

    private float startTimer;

    private Animator anim;

    public static FinishLine Instance;

    public bool isDone = false;

    

    private void Awake()
    {
        if(PlayerPrefs.GetInt("isFrenzy") == 1)
        {
            range = 0;
            timer = 6;
        }
        else
        {
            //range = 4;
            //timer = 10;
        }

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
                    if (!isDone)
                    {
                        GameManager.Instance.Win();
                        isDone = true;
                    }
                }
                else
                {
                    //BUG
                    if(GameManager.Instance.curHealth <= 0)
                    {
                        isDone = true;
                    }
                    GameManager.Instance.TakeDamage(1);

                }
            }
        }
    }


    public void StartCounting()
    {
        startcount = true;
    }
}
