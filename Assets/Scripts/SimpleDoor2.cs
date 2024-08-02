using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor2 : MonoBehaviour
{
    public GameObject Door;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("Sprite Changed");
        Destroy(Door);
    }
}
