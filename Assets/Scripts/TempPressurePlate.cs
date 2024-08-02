using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPressurePlate : MonoBehaviour
{
    public GameObject door;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Push")
        {
            door.SetActive(false);
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else{
            door.SetActive(true);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
