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
    public ScoreHud scoreScript;

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
    public float PJumpCD;
    //public bool PJumpReady;
    //public float PJumpReadyCD;
    public bool PJumpActivated;
    public float PJumpThreshold;
    public float newSpeed;
    public float speedBuff;
    public bool sprinting;
    public float SprintLeft;
    public float SprintDuration;

    private void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<ZyPlayerMove>();
        coll = GetComponent<Collision>();
        PJumpAvailable = false;
        //PJumpReady = true;
        slidecdOver = true;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreScript = FindObjectOfType<ScoreHud>();
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

            if (playerMove.jumpBufferCounter > -0.1f && PJumpAvailable) //If jumped activate pjump
            {
                StartCoroutine(PJumpActivate());
                PJumpAvailable = false;
            }
            else if (slideTimer <= 0f) // if 0 gg type shit
            {
                StopSlide();
                sprite.color = Color.white;
            }
        }
        /*else
        {
            PJumpAvailable = false;
        }*/

        //color change 
        if (!PJumpAvailable && !PJumpActivated)
        {
            sprite.color = Color.white;
        }
        else if (PJumpAvailable && slideTimer <= PJumpThreshold) //pjump available and ready
        {
            sprite.color = Color.red;
        }
        else if (PJumpActivated) //pjump type shi u feel?
        {
            PJumpAvailable = false;
            sprite.color = Color.yellow;
        }

        //SpeedCountdown
        if (SprintLeft > 0)
        {
            sprinting = true;
            //playerMove.speed = newSpeed;
            SprintLeft -= Time.deltaTime;
        }
        else if (SprintLeft <= 0)
        {
            sprinting = false;
            playerMove.speed = 15f;
            SprintLeft = 0f;
        }
        /*
        //PJump Cooldown
        if (PJumpCD > 0)
        {
            PJumpReady = false;
            PJumpCD -= Time.deltaTime;
        }
        else if (PJumpCD <= 0)
        {
            PJumpReady = true;
            particleScript.PJumpReady.Play();
            PJumpReadyCD = 4f;
        }
        
        //PJumpReadyCD, yea yea inefficient or wtv but idk how to code it better so...
        if (PJumpReadyCD > 0)
        {
            PJumpReadyCD -= Time.deltaTime;
        }
        else if (PJumpReadyCD <= 0)
        {
            PJumpReadyCD = 0;
        }*/
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
        //PJumpAvailable = true;
        StartCoroutine(PJumpAvailablething());
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
        //slideTimer = 0f;
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
        audioManager.PlayPJumpSfx(); //SOUND
        particleScript.PJumpBang.Play(); //PARTICLES    
        scoreScript.PJumpPoints();
        //SLOW TIME
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(.2f);
        //resume time
        Time.timeScale = 1f;
        slideTimer = 0f;
        //enter sprint
        IncreaseSpeed();
        if ((playerMove.speed + speedBuff) < 25f) //Buff Speed
        {
            playerMove.speed += speedBuff;
        }
        else if (playerMove.speed >25f) // if speed too high nah
        {
            playerMove.speed = 25f;
        }
        Debug.Log("SpeedIncreased");
        canSlide = true;
        PJumpActivated = false;
        PJumpCD = 4f;
        yield return null;
    }

    public IEnumerator PJumpAvailablething() //PJump available coroutine uk?
    {
        PJumpAvailable = true;
        yield return new WaitForSeconds(1f);
        PJumpAvailable = false;
    }
    //PJUMP SPEED BOOST 
    public void IncreaseSpeed()
    {
        //playerMove.speed = newSpeed; 
        PJumpAvailable = false;
        SprintLeft = SprintDuration;
        //sprinting = true;
        //playerMove.speed = 15f;
        //sprinting = false;
    }
}






    

 



