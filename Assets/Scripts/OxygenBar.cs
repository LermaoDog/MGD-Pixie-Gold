using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Image oxygenBar;
    public float maxOxygen;
    public float oxygenLeft;

    public ZyPlayerMove PlayerMove;
    public AdvancedSliding PlayerSlide;

    public IEnumerator Die;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        oxygenBar = GetComponent<Image>();
        oxygenLeft = maxOxygen;
        PlayerMove = FindObjectOfType<ZyPlayerMove>();
        PlayerSlide = FindObjectOfType<AdvancedSliding>();
    }

    // Update is called once per frame
    void Update()
    {
        //Continious depletion of oxygen code
        if (oxygenLeft > 0)
        {
            oxygenLeft -= Time.deltaTime;
            oxygenBar.fillAmount = oxygenLeft / maxOxygen;

            //IF OXYGEN GOES OVER LIMIT, SET TO MAX VALUE
            if (oxygenLeft > maxOxygen)
            {
                oxygenLeft = maxOxygen;
            }
            
        }
        else
        {
            PlayerMove.speed = 0f;
            PlayerMove.jumpVelocity = 0f;
            PlayerMove.enabled = false;
            PlayerSlide.enabled = false;

            StartCoroutine(PlayerMove.Die());

            this.enabled = false;
        }
    }
}


   
        


 



     
        
        
    
