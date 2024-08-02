using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticles;
    [SerializeField] ParticleSystem fallingParticles;
    public ParticleSystem slideLines;
    public ParticleSystem sprintLines;
    public ParticleSystem PJumpBang;
    [Range(0, 30)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.5f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    public Collision colScript;
    public AdvancedSliding slideScript;
    float counter;
    public LayerMask layer;
    int Ground;

    AudioManager audioManager;


    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Ground = LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (colScript.Grounded && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticles.Play();
                counter = 0;
            }
        }
        if (slideScript.sliding && slideScript.sprinting == false) //Slide Lines
        {
            slideLines.Play();  
        }
        else if (slideScript.sliding == false && slideScript.sprinting == false)
        {
            slideLines.Stop();
        }

        if (slideScript.sprinting) //Sprint Lines
        {
            sprintLines.Play();
        }
        else if (slideScript.sprinting == false)
        {
            sprintLines.Stop();
        }
        
        /*if (slideScript.PJumpActivated) //PJump effects
        {
            PJumpBang.Play();
        }
        else if (slideScript.PJumpActivated == false)
        { 
            PJumpBang.Stop(); 
        }*/
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layerMask = collision.gameObject.layer;
        if (layerMask == Ground)
        {
            fallingParticles.Play();
            //audioManager.PlayLandSfx();
        }
    }
}
