using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreHud : MonoBehaviour
{
    public GameObject SHud;
    public ZyPlayerMove moveScript;
    public AdvancedSliding slideScript;
    public SuppliesManager supplyScript;
    public TMP_Text currentScoreShown;
    public TMP_Text actionsText;
    public GameObject C;
    public GameObject B;
    public GameObject A;
    public GameObject S;


    public Image scoreBar;
    public float maxScore; //DEFAULT IS 300
    public float currentScore;
    public float actualScore;
    public float decreaseX; //DEFAULT IS 1
    public float graceTime; //DEFAULT IS 3

    private Queue<string> actions = new Queue<string> ();
    // Start is called before the first frame update
    void Start()
    {
        Image scoreBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (graceTime > 0) //Start countdown before decreasing score
        {
            graceTime -= Time.deltaTime;
        }

        if (actualScore > 0)
        {
            SHud.SetActive(true);

            if (graceTime <= 0)
            {
                actualScore -= decreaseX * Time.deltaTime;
                //scoreBar.fillAmount = actualScore / maxScore;
            }

            //IF SCORE GOES OVER RANGE CHANGE TEXT TO NEXT GRADE
            if (actualScore > 0 && actualScore <=50) //C
            {
                scoreBar.fillAmount = actualScore / (maxScore-250);
                decreaseX = 4f;
                S.SetActive(false);
                A.SetActive(false);
                B.SetActive(false);
                C.SetActive(true);
            }
            if (actualScore > 50 && actualScore <=100)//B
            {
                scoreBar.fillAmount = (actualScore-50) / (maxScore-200);
                decreaseX = 6f;
                S.SetActive(false);
                A.SetActive(false);
                B.SetActive(true);
                C.SetActive(false);
            } 
            if (actualScore > 100 && actualScore <=150)//A
            {
                scoreBar.fillAmount = (actualScore-100) /(maxScore-150);
                decreaseX = 8f;
                S.SetActive(false);
                A.SetActive(true);
                B.SetActive(false);
                C.SetActive(false);
            } 
            if (actualScore > 150)
            {
                scoreBar.fillAmount = (actualScore-150) / maxScore;//S
                decreaseX = 12f;
                S.SetActive(true);
                A.SetActive(false);
                B.SetActive(false);
                C.SetActive(false);
            }
        }
        else
        {
          SHud.SetActive(false);
        }

        currentScore = Mathf.Round(actualScore * 1f) * 1f; //Rounds actual score to current score to show on ui
        currentScoreShown.text = currentScore.ToString(); //change number for thingy
    }

    //POINTS 
    public void supplyPoints()
    { 
        actualScore += 5;
        graceTime = 1f;
        AddAction("+Supplies         +5");
        StartCoroutine(ActionCD());
    }   
    public void O2Points()
    {
        actualScore += 15;
        graceTime = 3f;
        AddAction("+O2 Tank         +15");
        StartCoroutine(ActionCD());
    }   
    public void PJumpPoints()
    {
        actualScore += 30;
        graceTime = 5f;
        AddAction("+Perfect Jump +30");
        StartCoroutine(ActionCD());
    }

    public IEnumerator ActionCD()
    {
        yield return new WaitForSeconds(1f);
        RunNextAction();
    }

    //QUEUE LIST OF ACTIONS
    public void AddAction(string action)
    {
        actions.Enqueue(action);
        UpdateQueuedActionText();
    }
    public void RunNextAction()
    {
        if (actions.Count == 0)
        {
            Debug.Log("No actions performed");
            return;
        }
        string act = actions.Dequeue();
        Debug.Log("acted");
        UpdateQueuedActionText();
    }
    void UpdateQueuedActionText()
    {
        actionsText.text = string.Empty;

        foreach (string action in actions)
        {
            actionsText.text += action + "  ";
        }
    }
}
