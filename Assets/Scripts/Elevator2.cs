using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator2 : MonoBehaviour
{
    public bool canMove;

    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform[] floorPoints;

    public GameObject player;
    public GameObject ElevatorUI;

    public AdvancedSliding slideScript;
    public ZyPlayerMove moveScript;
    Rigidbody2D rb;

    public int i;
    public bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = floorPoints[startPoint].position;
        i = startPoint;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, floorPoints[i].position) < 0.01f)
        {
            canMove = false;
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, floorPoints[i].position, speed * Time.deltaTime);
            moveScript.speed = 0f;           
            slideScript.enabled = false;
        }
        else
        {
            moveScript.enabled = true;
            slideScript.enabled = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision) //Activate UI and make player child of lift
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveScript.isOnLift = true;
            moveScript.liftRb = rb;
            collision.transform.SetParent(transform);
            ElevatorUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision) //Deactivate UI and blah blah 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveScript.isOnLift = false;
            collision.transform.SetParent(null);
            ElevatorUI.SetActive(false);
        }
    }

    public void Up()
    {
        if (!canMove)
        {
            canMove = true;

            if (i == floorPoints.Length - 1)
            {
                return;
            }
            else if (i == 0)
            {
                reverse = false;
                i++;
                return;
            }

            if (reverse)
            {
                i--;
            }
            else
            {
                i++;
            }
        }
        else if (canMove)
        {
            return;
        }
    }

    public void Down()
    {
        if (!canMove)
        {
            canMove = true;
            reverse = true;
            if (i == 0)
            {
                reverse = false;
                return;
            }
            else
            {
                i--;
            }
            reverse = false;
        }
        else if (canMove)
        {
            return;
        }
    }
}
