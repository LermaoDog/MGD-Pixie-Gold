using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public float yDecrease;
    public Vector3 originalPos;
    public Vector3 currentPos;
    public Vector3 lowestPos;
    public bool moveBack = false;
    public GameObject door;
    //public GameObject sensor;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    //Enable either moveBack or transform.parent
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Push")
        {
            moveBack = false;   
            collision.transform.parent = transform;
            transform.Translate(0, -yDecrease, 0);
        }
        /*else
        {
            if (collision.gameObject.tag = "sensor")
            {
                collision.transform.parent = true;
            }
        }
        */
    }

    //Enable either moveBack or transform.parent
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Push" && currentPos.y > lowestPos.y)
        {
            moveBack = false;
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
            door.SetActive(false);
        }
        /*
        else
        {
            if (collision.gameObject.tag == "sensor")
            {
                collision.transform.parent = true;
            }
        }
        */
    }
    
    //Enable either moveBack or transform.parent. Not both
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Push")
        {
            moveBack = true;
            transform.Translate(0, yDecrease, 0);
            collision.transform.parent = null;
            GetComponent<SpriteRenderer>().color = Color.white;
            door.SetActive(true);
        }
       
    }

    private void Update()
    {
        currentPos = transform.position;

        if (!moveBack)
        {
            if (transform.position.y != originalPos.y)
            {
                transform.Translate(0, -yDecrease, 0);
            }
        }
        else if (transform.position.y != originalPos.y)
        {
            transform.Translate(0, yDecrease, 0);
            GetComponent<SpriteRenderer>().color = Color.red;

        }
       
    }
}
