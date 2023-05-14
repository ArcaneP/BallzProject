using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private bool isDragging;

    Vector2 orgPos;

    private void Start()
    {
        this.gameObject.transform.position = orgPos;
    }

    public void OnMouseDown()
    {
        isDragging = true;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnMouseUp()
    {
        isDragging = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }

        if(
            (this.GetComponent<Rigidbody2D>().velocity.x >= 50 || this.GetComponent<Rigidbody2D>().velocity.x <= -50) 
            ||
            (this.GetComponent<Rigidbody2D>().velocity.y >= 50 || this.GetComponent<Rigidbody2D>().velocity.y <= -50)
          )
        {
            this.gameObject.transform.position = orgPos;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}