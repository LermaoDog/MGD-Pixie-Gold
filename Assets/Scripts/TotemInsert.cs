using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemInsert : MonoBehaviour
{
    public GameObject switchOn;
    public bool hasTotem;
    public GameObject TempleEntrance;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasTotem)
        { 
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
            Destroy(TempleEntrance);
        }
        else
        {
            Debug.Log("You need the Totem!");
        }
    }
}
