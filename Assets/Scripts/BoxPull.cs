using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public bool beingPushed;
    float xPos;
    public GameObject pushPullScreen;
    public GameObject player;

    //Dont add multiple pushPullScreen gambeObject into different scripts
    //public float renderDist;
    //public float gd;

    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //gd = Vector2.Distance(player.transform.position, transform.position);
        if (beingPushed == false)
        {
            transform.position = new Vector3(xPos, transform.position.y);
        }
        else
        {
            xPos = transform.position.x;
        }

        /*
        if (gd < renderDist)
        {
            pushPullScreen.SetActive(true);
        }
        else if (gd > renderDist) 
        {
            pushPullScreen.SetActive(false);
        }
        */
    }
}
