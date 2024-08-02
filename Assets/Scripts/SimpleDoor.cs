using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject changedSprite;

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = changedSprite.GetComponent<SpriteRenderer>().sprite;
        Debug.Log("Sprite Changed");
        Destroy(Door);
    }
}
