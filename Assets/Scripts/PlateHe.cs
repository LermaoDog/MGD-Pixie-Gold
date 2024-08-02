using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateHe : MonoBehaviour
{
    public FinalTempleDoor doorScript;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Push - Di")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            doorScript.plateHe = false;
        }
        else if (collision.transform.tag == "Push - Shui")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            doorScript.plateTian = true;
            doorScript.plateHe = true;
        }
        else if (collision.transform.tag == "Push - Huo")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            doorScript.plateTian = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Push - Di")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            doorScript.plateTian = false;
        }
        else if (collision.transform.tag == "Push - Shui")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            doorScript.plateTian = false;
        }
        else if (collision.transform.tag == "Push - Huo")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            doorScript.plateTian = false;
        }
    }
}
