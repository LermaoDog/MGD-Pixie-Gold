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
    public AdvancedSliding slideScript;
    public ZyPlayerMove moveScript;
    public OxygenBar O2;
    private GameObject O2PU;
    public Joystick joystick; //Joystick
    AudioManager audioManager;
    public ScoreHud scoreScript;

    [Header("Horizontal movement")]
    public float speed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public float currentSpeed;  //New Addition

    [Header("Jump settings")]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool isJumping;  //Old is canJump;
    //double jumps
    private float currentJumpCounter;
    public float additionalJumps;
    //Coyote time and jump buffering
    public float coyoteTime;
    public float coyoteTimeCounter;
    public float jumpBufferTime;
    public float jumpBufferCounter;

    [Header("Tp settings")]
    public bool canTP;


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
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        slideScript = GetComponent<AdvancedSliding>();
        scoreScript = FindObjectOfType<ScoreHud>();
        coll.Grounded = true;
        canTP = true;
    }

    private void Update()
    {
        //New Addition
        currentSpeed = rb.velocity.x + rb.velocity.y;

        O2PU = GameObject.FindGameObjectWithTag("O2");
        //horizontal = Input.GetAxisRaw("Horizontal"); //Keyboard and Mouse movement
        horizontal = joystick.Horizontal;  //Joystick Movement

        anim.SetFloat("Speed", Mathf.Abs(horizontal)); //Make it so Speed is always +ive

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
            jumpBufferCounter -= Time.deltaTime;
       
        //CHECK IF GROUNDED
        if (coll.Grounded || isOnLift)
        {
            anim.SetBool("Grounded", true);
            currentJumpCounter = 0;
            isJumping = false;
            //decceleration = 16f;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            anim.SetBool("Grounded", false);
            //decceleration = 0f;
            coyoteTimeCounter -= Time.deltaTime;
        }

        Flip();

    }
    private void FixedUpdate()
    {
            //BOTH M&K AND ANDROID MOVEMENT
            float targetSpeed = horizontal * speed;
            float speedDiff = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

            //rb.AdForce changed (time.deltatime added)
            rb.AddForce(movement * Vector2.right * Time.fixedDeltaTime);
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
        //Lots of things changed from old to new in jump
        jumpBufferCounter = jumpBufferTime;
            
        if ((coyoteTimeCounter > 0f && jumpBufferCounter > 0f) || currentJumpCounter < additionalJumps)
        {
            slideScript.sliding = false;
            isJumping = true;
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
            
        }
        
    }
    

    public IEnumerator Die() //Play Death and disable player
    {
        audioManager.PlayDeathSfx();
        anim.SetBool("IsDead", true);
        //GetComponent<Rigidbody2D>().simulated = false;
        this.enabled = false;
        yield return new WaitForSeconds(3.2f);
        Debug.Log("Player died!");
        gameOverScreen.SetActive(true);
        restartButton.SetActive(true);
        Time.timeScale = 1;
    }

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
            victoryScreen.SetActive(true);
            nextLevelButton.SetActive(true);
            GetComponent<Rigidbody2D>().simulated = false;
            //moveScript.enabled = false;
            slideScript.enabled = false;
            speed = 0f;
        }
    }
}
