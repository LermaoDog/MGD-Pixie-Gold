using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject sender;
    public GameObject reciever;
    private GameObject player;
    public ZyPlayerMove moveScript;
    public float tpDistLimit;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        moveScript = FindObjectOfType<ZyPlayerMove>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > tpDistLimit)
            {
                //moveScript.canTP = false;
                player.transform.position = new Vector2(reciever.transform.position.x, reciever.transform.position.y);
                //moveScript.canTP = true;
            }
        }
    }
}