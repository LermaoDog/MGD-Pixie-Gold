using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OxygenTank : MonoBehaviour
{
    public OxygenBar O2;
    public ScoreHud scoreScript;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        O2 = FindObjectOfType<OxygenBar>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreScript = FindObjectOfType<ScoreHud>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) //Pickup O2
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("SlideCol"))
        {
            O2.oxygenLeft += 15;
            scoreScript.O2Points();
            //O2.oxygenBar.color = Color.white;
            //StartCoroutine(ColorChangeDuration());
            audioManager.PlaySFX(audioManager.o2Refill);
            Destroy(gameObject);
            Debug.Log("Added o2");
        }
    }

    /*private IEnumerator ColorChangeDuration()
    {
        yield return new WaitForSeconds(1f);
        O2.oxygenBar.color = Color.blue;
    }*/
}
