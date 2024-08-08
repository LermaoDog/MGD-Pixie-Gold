using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TMP_Text levelTimeShown;

    public float levelTime;
    public int publicMinutes;
    public int publicSeconds;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(levelTime / 60);
        int seconds = Mathf.FloorToInt(levelTime % 60);
        //levelTimeRounded = Mathf.Round(levelTime * 1f) * 1f;
        //levelTimeShown.text = levelTimeRounded.ToString();
        levelTimeShown.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        publicMinutes = minutes;
        publicSeconds = seconds;
    }
}
