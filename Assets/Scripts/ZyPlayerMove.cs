using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ZyPlayerMove : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;
    private Collision coll;
    public SpriteRenderer sprite;
    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;
    public GameObject gameOverScreen;
    public GameObject restartButton;
    public GameObject victoryScreen;
    public GameObject nextLevelButton;
    public GameObject winScreen;
    public AdvancedSliding slideScript;
    public ZyPlayerMove moveScript;
    public OxygenBar O2;
    private GameObject O2PU;
    public Joystick joystick; //Joystick
    AudioManager audioManager;
    public ScoreHud scoreScript;
    public WinScreen winScript;
    public GameObject SHudParent;
    public GameObject levelTimeParent;
    public GameObject supplyParent;
    public GameObject controlsParent;
    public GameObject joystickParent;
    public GameObject O2Bar;

    [Header("Movement")]
    public float speed;
    private  float acceleration = 900f;
    private  float decceleration = 1200f;
    private float velPower = 0.9f;
    public float currentSpeed;  //New Addition

    public float desiredSpeed;
    public float lastSpeed;
    public float airMultiplier;

    [Header("Jump settings")]
    public float jumpVelocity;
    public float fallMultiplier = 14f;
    private float lowJumpMultiplier = 3f;
    //public bool isJumping;  //Old is canJump;
    //double jumps
    private float currentJumpCounter;
    public float additionalJumps;
    //Coyote time and jump buffering
    private float coyoteTime =0.13f;
    public float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;

    [Header("Tp settings")]
    public bool canTP;
    private bool canTPcd;


    //New Addition
    [Header("Misc stuff")]
    private bool isFacingRight = true;
    private float horizontal;
    public bool isOnLift;
    public Rigidbody2D liftRb;


    public TotemInsert totemInsert;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        slideScript = GetComponent<AdvancedSliding>();
        scoreScript = FindObjectOfType<ScoreHud>();
        coll.Grounded = true;
        canTP = true;
        canTPcd = true;
    }

    private void Update()
    {
        //New Addition
        currentSpeed = rb.velocity.x;
        O2PU = GameObject.FindGameObjectWithTag("O2");
        //horizontal = Input.GetAxisRaw("Horizontal"); //Keyboard and Mouse movement
        horizontal = joystick.Horizontal;  //Joystick Movement
        anim.SetFloat("Speed", Mathf.Abs(horizontal)); //Make it so Speed is always +ive

        if (!canTP && canTPcd)
        {
            canTPcd = false;
            //StartCoroutine(TPcdOver());
            Invoke("TPcdOver", 1.7f);
        }

        //If rising, low grav. If falling, high grav
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        //JUMP INPUT PCs
        if (Input.GetButtonDown("Jump"))
        {
            
            Jump();
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

       
        //CHECK IF GROUNDED
        if (coll.Grounded || isOnLift)
        {
            anim.SetBool("Grounded", true);
            currentJumpCounter = 0;
            //isJumping = false;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            anim.SetBool("Grounded", false);
            coyoteTimeCounter -= Time.deltaTime;
        }

        //check if desiredSpeed has changed drastically
        if(Mathf.Abs(desiredSpeed - lastSpeed) >3.2f && speed != 0)
        {
            //StopAllCoroutines();
            StartCoroutine(SmoothlyLerpSpeed());
        }
        else
        {
            speed = desiredSpeed;
        }

        lastSpeed = desiredSpeed;
        Flip();

    }
    private void FixedUpdate()
    {
        //calculations for acceleration
        float targetSpeed = horizontal * speed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

        //move the player
        if (coll.Grounded)
        {
            rb.AddForce(movement * Vector2.right * Time.fixedDeltaTime);
        }
        else if (!coll.Grounded)
        {
            rb.AddForce(movement * airMultiplier * Vector2.right * Time.fixedDeltaTime);
        }
    }
    private void Flip() //Flip Player to face correct direction
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    //JUMP function
    public void Jump()
    {
        jumpBufferCounter = jumpBufferTime;     
        //Lots of things changed from old to new in jump
        if ((coyoteTimeCounter > 0.1f && jumpBufferCounter > 0f) || currentJumpCounter < additionalJumps)
        {
            coyoteTimeCounter = 0f;
            //isJumping = true;
            currentJumpCounter += 1; //Double jump counter
            anim.SetTrigger("Jumped");
            regularColl.enabled = true;  //change hitbox
            slideColl.enabled = false;
            //GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity; [Slows Player when jumping, so byebye]
            float force = jumpVelocity; //Jump fix idk why but it works so idk
            if (rb.velocity.y < 0)
            {
                force -= rb.velocity.y;
            }
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            jumpBufferCounter = 0f;         

            /*if (slideScript.sliding)
            {
                rb.AddForce(Vector2.right * rb.velocity.x, ForceMode2D.Impulse);
            }*/
            slideScript.sliding = false;
        }      
    }
    
    public void TPcdOver()
    {
        //yield return new WaitForSeconds(2.5f);
        canTP = true;
        canTPcd = true;
        Debug.Log("TPcd over");
    }

    public IEnumerator Die() //Play Death and disable player
    {
        Time.timeScale = 1f;
        audioManager.PlayDeathSfx();
        anim.SetBool("IsDead", true);
        //GetComponent<Rigidbody2D>().simulated = false;
        SHudParent.SetActive(false);
        levelTimeParent.SetActive(false);
        supplyParent.SetActive(false);
        controlsParent.SetActive(false);
        joystickParent.SetActive(false);
        O2Bar.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.gravityScale = 10f;
        StartCoroutine(GameOver());
        Debug.Log("Player died!");
        yield break;
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.7f);
        gameOverScreen.SetActive(true);
        restartButton.SetActive(true);
        Debug.Log("game over");
        yield break;
    }

    private IEnumerator SmoothlyLerpSpeed()
    {
        float time = 0f;
        float difference = Mathf.Abs(desiredSpeed - speed);
        float startValue = speed;

        while (time < difference)
        {
            speed = Mathf.Lerp(startValue, desiredSpeed, time / difference);
            time += Time.deltaTime * 2f;
            yield return null;
        }
        speed = desiredSpeed;
    }

    /*private IEnumerator StopSpeed()
    {
        float time = 0f;
        float difference = Mathf.Abs(desiredSpeed - speed);
        float startValue = speed;

        while (time < difference)
        {
            speed = Mathf.Lerp(startValue, desiredSpeed, time / difference);
            time += Time.deltaTime * 30f;
            yield return null;
        }
        speed = desiredSpeed;
        yield return new WaitForSeconds(0.5f);
    }*/

    private void OnTriggerEnter2D(Collider2D collision) //Pickup O2
    {
        if (collision.gameObject.CompareTag("Supplies"))
        {
            audioManager.PlaySFX(audioManager.suppliesPickup);
            Destroy(collision.gameObject);
            Debug.Log("Added Supply");
        }
        else if (collision.gameObject.CompareTag("Totem"))
        {
            audioManager.PlaySFX(audioManager.scrap);
            Destroy(collision.gameObject);
            totemInsert.hasTotem = true;
            Debug.Log("Totem Collected");
        }
        else if (collision.gameObject.CompareTag("End"))
        {
            //Add function for win screen here
            winScreen.SetActive(true);
            //victoryScreen.SetActive(true);
            //nextLevelButton.SetActive(true);
            //GetComponent<Rigidbody2D>().simulated = false;
            //desiredSpeed = 0f;
            //lastSpeed = desiredSpeed;
            //StartCoroutine(StopSpeed());
            //moveScript.enabled = false;
            //slideScript.enabled = false;
        }
    }
}
