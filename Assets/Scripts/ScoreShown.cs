using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreShown : MonoBehaviour
{
    [Header("References")]
    public ScoreHud SHud;
    public Shake shakeScript;
    public TMP_Text currentScoreShown;

    [Header("ScoreSettings")]
    public float currentScore;
    public float actualScore;
    // Start is called before the first frame update
    void Start()
    {
        SHud = FindObjectOfType<ScoreHud>();
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = Mathf.Round(SHud.actualScore * 1f) * 1f; //Rounds actual score to current score to show on ui
        currentScoreShown.text = currentScore.ToString();//show text

        //GRADE STATE CHANGES VIBRATION STRENGTH
        if (currentScore > 0 && currentScore <=50) //C
        {
            shakeScript.strength = 1f;
        }
        if (currentScore > 50 && currentScore <=100) //B
        {
            shakeScript.strength = 2f;
        }
        if (currentScore > 100 && currentScore <=150) //A
        {
            shakeScript.strength = 3f;
        }
        if (currentScore > 150) //S+
        {
            shakeScript.strength = 4f;
        }
    }
}
