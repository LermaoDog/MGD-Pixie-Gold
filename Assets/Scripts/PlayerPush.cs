using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    public GameObject pushPull;
    public ZyPlayerMove moveScript;

    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        pushPull.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
        Physics2D.queriesStartInColliders = false;

        if (hit.collider != null && (hit.collider.gameObject.tag == "Push" || hit.collider.gameObject.tag == "Push - Shui" || hit.collider.gameObject.tag == "Push - Huo" || hit.collider.gameObject.tag == "Push - Di"))
        {
            pushPull.SetActive(true);
            Debug.Log("ray hit");
        }
        else
        {
            pushPull.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartPush();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            StopPush();
        }


        /*
        if (hit.collider != null && hit.collider.gameObject.tag == "Push" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pushing Box Active");
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<BoxPull>().beingPushed = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
        }
        */
    }

    //Object detection for character
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
    }

    public void StartPush()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && (hit.collider.gameObject.tag == "Push" || hit.collider.gameObject.tag == "Push - Shui" || hit.collider.gameObject.tag == "Push - Huo" || hit.collider.gameObject.tag == "Push - Di"))
        {
            Debug.Log("Pushing Box Active");
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<BoxPull>().beingPushed = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            hit.collider.transform.SetParent(transform);

            moveScript.jumpVelocity = 32;
        }
        /* else if (hit.collider == null && hit.collider.gameObject.tag == "Push")
         {
             StopPush();
         }
         */

    }

    public void StopPush()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && (hit.collider.gameObject.tag == "Push" || hit.collider.gameObject.tag == "Push - Shui" || hit.collider.gameObject.tag == "Push - Huo" || hit.collider.gameObject.tag == "Push - Di"))
        {
            Debug.Log("Pushing Box inActive");
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<BoxPull>().beingPushed = false;
            hit.collider.transform.SetParent(null);

            moveScript.jumpVelocity = 16;
        }
    }

}
