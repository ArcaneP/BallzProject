using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravType    
{
    minus,plus
}

public class GravitySwitcher : MonoBehaviour
{
    public GravType type;

    public float gravscale = 0.25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (type)
        {
            case GravType.minus:
                Debug.Log("minus");
                other.GetComponent<Rigidbody2D>().gravityScale = gravscale;
                break;
            case GravType.plus:
                Debug.Log("plus");
                other.GetComponent<Rigidbody2D>().gravityScale = -gravscale;
                break;
        }
    }
}