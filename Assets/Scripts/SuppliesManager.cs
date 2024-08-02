using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;

public class SuppliesManager : MonoBehaviour
{
    public static SuppliesManager Instance;
    //public TextMeshProUGUI text;
    public Text texttemp;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeScore(int suppliesValue)
    {
        score += suppliesValue;
        //text.text = "X" + score.ToString();
        texttemp.text = "X" + score.ToString();
    }
}