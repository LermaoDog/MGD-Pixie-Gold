using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [Header("References")]
    public GameObject winScreen;
    public GameObject SHudParent;
    public GameObject levelTimeParent;
    public GameObject supplyParent;
    public GameObject controlsParent;
    public GameObject joystick;
    public GameObject O2Bar;
    AudioManager audioManager;
    public GameObject nextLevelButton;

    [Header("TimeStuff")]
    public GameObject TimeParent; //FinalTimeShown obj
    public TMP_Text finalTimeShown; //FinalTimeNumber Tmp
    public LevelTimer timerScript; //LevelTimer Script
    public GameObject TimeS; //Time Grade obj
    public GameObject TimeA;
    public GameObject TimeB;
    public GameObject TimeC;
    public float MaxTime;
    public float STime; //Time required to get S, A, & so on
    public float ATime;
    public float BTime;
    public float CTime;
    public GameObject timeGradeImage;
    
    

    [Header("O2Stuff")]
    public GameObject O2Parent; //O2 Stuff
    public TMP_Text finalO2Shown; 
    public OxygenBar O2Script;
    public GameObject O2S;
    public GameObject O2A;
    public GameObject O2B;
    public GameObject O2C;    
    public float SO2;
    public float AO2;
    public float BO2;
    public float CO2;
    public GameObject O2GradeImage;

    [Header("ScoreStuff")]
    public GameObject ScoreParent; //Score Stuff
    public TMP_Text finalScoreShown; 
    public ScoreShown scoreShowScript;
    public GameObject ScoreSplus;
    public GameObject ScoreS;
    public GameObject ScoreA;
    public GameObject ScoreB;
    public GameObject ScoreC;
    public GameObject ScoreF;
    public float SScore;
    public float AScore;
    public float BScore;
    public float CScore;
    public GameObject scoreGradeImage;

    [Header("Supply Stuff")]
    public GameObject SupplyParent; //Supply Stuff
    public TMP_Text finalSupplyShown;
    public SuppliesManager supplyScript;
    public GameObject SupplyS;
    public GameObject SupplyA;
    public GameObject SupplyB;
    public GameObject SupplyC;
    public GameObject SupplyF;
    public float SSupply;
    public float ASupply;
    public float BSupply;
    public float CSupply;
    public GameObject supplyGradeImage;

    [Header("TotalPoints")]
    public float TotalPoints;
    public float FinalPoints;
    public GameObject TotalPointsParent;
    public TMP_Text totalPointsShown;
    public GameObject TotalPointsBg;
    public GameObject TotalGradeS;
    public GameObject TotalGradeA;
    public GameObject TotalGradeB;
    public GameObject TotalGradeC;
    public GameObject TotalGradeF;
    public float SPoints;
    public float APoints;
    public float BPoints;
    public float CPoints;

    [Header("Values")]
    public float finalTime;
    public float finalO2Rounded;
    public float finalScore;
    public float finalSupplies;
    // Start is called before the first frame update
    void Start()
    {
        //Reference stuff
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        SHudParent.SetActive(false);
        levelTimeParent.SetActive(false);
        supplyParent.SetActive(false);
        controlsParent.SetActive(false);
        joystick.SetActive(false);
        O2Bar.SetActive(false);

        //SET VALUES AT START
        finalTime = timerScript.levelTime;
        finalO2Rounded = Mathf.Round(O2Script.oxygenLeft * 1f) * 1f;
        finalScore = scoreShowScript.currentScore;
        finalSupplies = supplyScript.score;

        //PUT VALUES INTO TEXT
        //finalTimeShown.text = string.Format("{0:00}:{1:00}",timerScript.publicMinutes , timerScript.publicSeconds);
        //finalO2Shown.text = string.Format("{0}%", finalO2Rounded);
        //finalScoreShown.text = finalScore.ToString();
        //finalSupplyShown.text = finalSupplies.ToString();

        //Grade points formula = [(supply x 3) + score + (300s - current seconds)] * percent of oxygen left, then after that round it off
        SPoints = Mathf.Round(((SSupply * 3f + (SScore + 150f) + MaxTime - STime) * SO2/100f) *1f) *1f; 
        APoints = Mathf.Round(((ASupply * 3f + (SScore + 100f) + MaxTime - ATime) * AO2/100f) *1f) *1f;
        BPoints = Mathf.Round(((BSupply * 3f + (AScore + 50f) + MaxTime - BTime) * BO2/100f) *1f) *1f;
        CPoints = Mathf.Round(((BSupply * 3f + (BScore + 50f) + MaxTime - CTime) * CO2/100f) *1f) *1f;


        //YEA UHUH UK WHAT IT IS BLACK&YELLOW BLACK&YELLOW BLACK&YELLOW (fr tho)
        StartCoroutine(ShowTime());
    }

    //TIME STATS 
    public IEnumerator ShowTime()
    {
        yield return new WaitForSeconds(0.5f); //start delay

        if (!TimeParent.activeInHierarchy) //if time not active set active
        {
            TimeParent.SetActive(true);
            audioManager.PlayBluntPJumpSfx();
        }
        //SLOWLY INCREASE UNTIL FINAL TIME
        float timecurrentValue = 0f;
        var rate = Mathf.Abs(timerScript.levelTime - timecurrentValue) / 0.7f;
        while (timecurrentValue != finalTime)
        {
            timecurrentValue = Mathf.MoveTowards(timecurrentValue, timerScript.levelTime, rate * Time.deltaTime);
            finalTimeShown.text = string.Format("{0:00}:{1:00}", timecurrentValue/60, timecurrentValue % 60);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        //TIME GRADE SECTION
        timeGradeImage.SetActive(true);
        if (timerScript.levelTime < STime)
        {
            TimeS.SetActive(true);
        }
        else if (timerScript.levelTime > STime && timerScript.levelTime < ATime)
        {
            TimeA.SetActive(true);
        }
        else if (timerScript.levelTime > ATime && timerScript.levelTime < BTime)
        {
            TimeB.SetActive(true);
        }
        else if (timerScript.levelTime > BTime)
        {
            TimeC.SetActive(true);
        }
        TotalPoints += (MaxTime - timerScript.levelTime); //Add to total points
        audioManager.PlayPJumpSfx();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ShowO2());
    }

    //O2 STATS
    public IEnumerator ShowO2()
    {
        if (TimeParent.activeInHierarchy) //if time active set o2 active
        {
            O2Parent.SetActive(true);
            audioManager.PlayBluntPJumpSfx();
        }
        //SLOWLY INCREASE UNITL FINAL O2
        float o2currentValue = 0f;
        var rate = Mathf.Abs(finalO2Rounded - o2currentValue) / 0.7f;
        while (o2currentValue != finalO2Rounded)
        {
            o2currentValue = Mathf.MoveTowards(o2currentValue, finalO2Rounded, rate * Time.deltaTime);
            finalO2Shown.text = string.Format("{0}%", Mathf.Round(o2currentValue* 1f) * 1f);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        //O2 GRADE SECTION
        O2GradeImage.SetActive(true);
        if (finalO2Rounded > SO2)
        {
            O2S.SetActive(true);
        }
        else if (finalO2Rounded < SO2 && finalO2Rounded > AO2)
        {
            O2A.SetActive(true);
        }
        else if (finalO2Rounded < AO2 && finalO2Rounded > BO2)
        {
            O2B.SetActive(true);
        }
        else if (finalO2Rounded <= BO2)
        {
            O2C.SetActive(true);
        }
        audioManager.PlayPJumpSfx();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ShowScore());
    }

    //SCORE STATS
    public IEnumerator ShowScore()
    {
        if (O2Parent.activeInHierarchy) //if o2 active set score active
        {
            ScoreParent.SetActive(true);
            audioManager.PlayBluntPJumpSfx();
        }
        //SLOWLY INCREASE UNITL FINAL SCORE
        float scoreCurrentValue = 0f;
        var rate = Mathf.Abs(finalScore - scoreCurrentValue) / 0.7f;
        if (finalScore > 0f)
        {
            while (scoreCurrentValue != finalScore)
            {
                scoreCurrentValue = Mathf.MoveTowards(scoreCurrentValue, finalScore, rate * Time.deltaTime);
                finalScoreShown.text = (Mathf.Round(scoreCurrentValue * 1f) * 1f).ToString();
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }
        else if (finalScore <=0f)
        {
            yield return new WaitForSeconds(0.9f);
        }
        //SCORE GRADE SECTION
        scoreGradeImage.SetActive(true);

        if(scoreShowScript.currentScore >200f)
        {
            ScoreSplus.SetActive(true);
        }     
        else if (scoreShowScript.currentScore > SScore && scoreShowScript.currentScore <=200f)
        {
            ScoreS.SetActive(true);
        }
        else if (scoreShowScript.currentScore < SScore && scoreShowScript.currentScore > AScore)
        {
            ScoreA.SetActive(true);
        }
        else if (scoreShowScript.currentScore < AScore && scoreShowScript.currentScore > BScore)
        {
            ScoreB.SetActive(true);
        }
        else if (scoreShowScript.currentScore < BScore && scoreShowScript.currentScore > CScore)
        {
            ScoreC.SetActive(true); 
        }
        else if (scoreShowScript.currentScore <= CScore)
        {
            ScoreF.SetActive(true);
        }
        TotalPoints += scoreShowScript.currentScore; //Add 100% of score to total points
        audioManager.PlayPJumpSfx();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ShowSupplies());
    }

    //SUPPLY STATS
    public IEnumerator ShowSupplies()
    {
        if (ScoreParent.activeInHierarchy) //if score active set supply active
        {
            SupplyParent.SetActive(true);
            audioManager.PlayBluntPJumpSfx();
        }
        //SLOWLY INCREASE UNITL FINAL SUPPLY
        float supplyCurrentValue = 0f;
        var rate = Mathf.Abs(finalSupplies - supplyCurrentValue) / 0.7f;
        if (finalSupplies > 0f) //if supply more than 0 then do like normal but if <=0 then take the same time as others
        {
            while (supplyCurrentValue != finalSupplies)
            {
                supplyCurrentValue = Mathf.MoveTowards(supplyCurrentValue, finalSupplies, rate * Time.deltaTime);
                finalSupplyShown.text = (Mathf.Round(supplyCurrentValue * 1f) * 1f).ToString();
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }
        else if (finalSupplies <= 0f)
        {
            yield return new WaitForSeconds(0.9f);
        }
        //SUPPLY GRADE SECTION
        supplyGradeImage.SetActive(true);
        if (supplyScript.score >= SSupply)
        {
            SupplyS.SetActive(true);
        }
        else if (supplyScript.score < SSupply && supplyScript.score > ASupply)
        {
            SupplyA.SetActive(true);
        }
        else if (supplyScript.score < ASupply && supplyScript.score > BSupply)
        {
            SupplyB.SetActive(true);
        }
        else if (supplyScript.score < BSupply && supplyScript.score >= CSupply)
        {
            SupplyC.SetActive(true);
        }
        else if (supplyScript.score < CSupply)
        {
            SupplyF.SetActive(true);
        }
        TotalPoints += (supplyScript.score * 3f); //Add 300% of supply to total points
        audioManager.PlayPJumpSfx();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowFinalGrade());
    }

    //FINAL GRADE TYPE SHI
    public IEnumerator ShowFinalGrade()
    {
        //Show big grade stuff 
        FinalPoints = TotalPoints * finalO2Rounded / 100f;
        TotalPointsParent.SetActive(true); //point parent
        TotalPointsBg.SetActive(true); //point bg
        audioManager.PlayBluntPJumpSfx();
        //SLOWLY INCREASE UNITL FINAL POINT
        float pointCurrentValue = 0f;
        var rate = Mathf.Abs(FinalPoints - pointCurrentValue) / 1f;
        while (pointCurrentValue != FinalPoints)
        {
            pointCurrentValue = Mathf.MoveTowards(pointCurrentValue, FinalPoints, rate * Time.deltaTime);
            totalPointsShown.text = string.Format("{0}p", Mathf.Round(pointCurrentValue * 1f) * 1f);
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);

        if (FinalPoints >= SPoints)
        {
            TotalGradeS.SetActive(true);
        }
        else if (FinalPoints < SPoints && FinalPoints >= APoints)
        {
            TotalGradeA.SetActive(true);
        }
        else if (FinalPoints < APoints && FinalPoints >= BPoints)
        {
            TotalGradeB.SetActive(true);
        }
        else if (FinalPoints < BPoints && FinalPoints >= CPoints)
        {
            TotalGradeC.SetActive(true);
        }
        else if (FinalPoints < CPoints)
        {
            TotalGradeF.SetActive(true);
        }
        audioManager.PlayPJumpSfx();
        yield return new WaitForSeconds(1f);
        //show next level button
        nextLevelButton.SetActive(true);
        audioManager.PlayPJumpSfx();
    }
}
