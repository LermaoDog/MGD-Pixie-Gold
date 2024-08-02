using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedSliding : MonoBehaviour
{
    [Header("References")]
    public Transform playerObj;
    private Rigidbody2D rb;
    public Animator anim;
    private Collision coll;
    private ZyPlayerMove playerMove;
    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;
    private float horizontal;
    public Joystick joystick; //Joystick
    public OxygenBar O2;
    public SpriteRenderer sprite;
    public ParticleController particleScript;
    AudioManager audioManager;

    [Header("Slide settings")]
    public bool canSlide;
    public bool sliding;
    public float maxSlideTime;
    public float slideTimer;
    public float slideForce;
    public float slidecd;
    public bool slidecdOver;
    public KeyCode slideKey = KeyCode.LeftShift;

    [Header("PJump settings")]
    public bool PJumpAvailable;
    public bool PJumpActivated;
    public float PJumpThreshold;
    public float newSpeed;
    public bool sprinting;

    private void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<ZyPlayerMove>();
        coll = GetComponent<Collision>();
        PJumpAvailable = false;
        slidecdOver = true;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void Update()
    {
        horizontal = joystick.Horizontal; //Joystick Movement
        if (Input.GetKeyDown(slideKey) && (horizontal != 0) && canSlide)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && sliding)
        {
            StopSlide();
        }      

        if (sliding) //If slide started, change to sliding
            SlidingMovement();

        //Slide availability check
        if ((coll.Grounded == true) && slidecdOver == true)
        {
            canSlide = true;
        }
        else
        {
            canSlide = false;
        }

        //Perfect Movement check
        if (slideTimer <= PJumpThreshold && slideTimer > 0f) //if slide timer less than threshold and more than 0
        {
             Debug.Log("PJumpAvailable");

            if (playerMove.jumpBufferCounter > -0.1f && PJumpAvailable) //If jumped activate pjump
            {
                StartCoroutine(PJumpActivate());
                PJumpAvailable = false;
            }
            else if (slideTimer <= 0.1f)
            {
                StopSlide();
                sprite.color = Color.white;
            }
        }
        else
        {
            PJumpAvailable = false;
        }

        //color change 
        if (!PJumpAvailable && !PJumpActivated)
        {
            sprite.color = Color.white;
        }
        else if (PJumpAvailable && !PJumpActivated) //pjump available
        {
            sprite.color = Color.red;
        }
        else if (PJumpActivated) //pjump type shi u feel?
        {
            PJumpAvailable = false;
            sprite.color = Color.yellow;
        }
        
    }

    //ACTIVATE SLIDE
    public void slidePressed() 
    {
        if ((horizontal != 0) && canSlide)
        { 
          StartSlide();
          canSlide = false;
        }
    }

    //DEACTIVATE SLIDE
    public void slideReleased() 
    {
        if (sliding)
        {
            canSlide = false;
            StopSlide();
        }
    }

    //START SLIDE
    public void StartSlide()
    {
        slidecdOver = false;
        Debug.Log("SlideStarted");
        sliding = true;
        anim.SetTrigger("StartSlide");
        regularColl.enabled = false;  //change hitbox
        slideColl.enabled = true;
        slideTimer = maxSlideTime;
        StartCoroutine(SlideCD());
    }

    //SLIDING
    private void SlidingMovement() 
    {
        Debug.Log("Sliding");
        anim.SetBool("Sliding", true);
        Vector2 inputDirection = Vector2.right * horizontal;
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode2D.Force);
        PJumpAvailable = true;
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0f)
        {
           StopSlide();
        }

    }

    //STOP SLIDE
    public void StopSlide() 
    {
        Debug.Log("SlideStopped");
        regularColl.enabled = true;  //change hitbox
        slideColl.enabled = false;
        sliding = false;
        anim.SetBool("Sliding", false);
        anim.SetTrigger("EndSlide");
        slideTimer = 0f;
        Debug.Log("CD started");
        
    }

    //SLIDE COOLDOWN
    private IEnumerator SlideCD() 
    {
        yield return new WaitForSeconds(slidecd);
        slidecdOver = true;
        canSlide = true; 
    }

    //PJUMP FUNCTION
    public IEnumerator PJumpActivate()
    {
        PJumpActivated = true;
        Debug.Log("PJumpActivated");
        sprite.color = Color.yellow; //Change eye to show activation
        audioManager.PlayPJumpSfx();
        particleScript.PJumpBang.Play();
        //SLOW TIME
        Time.timeScale = 0.2f; 
        yield return new WaitForSeconds(.2f);
        //resume time
        Time.timeScale = 1f;
        slideTimer = 0f;
        //enter sprint
        StartCoroutine(IncreaseSpeed());
        canSlide = true;
        PJumpActivated = false;
        yield return null;

    }

    //PJUMP SPEED BOOST 
    public IEnumerator IncreaseSpeed()
    {
        playerMove.speed = newSpeed; //increase speed
        PJumpAvailable = false;
        sprinting = true;
        Debug.Log("SpeedIncreased");
        yield return new WaitForSeconds(3f);
        playerMove.speed = 15f;
        sprinting = false;
        Debug.Log("SpeedDecreased");
    }
}






    

 



