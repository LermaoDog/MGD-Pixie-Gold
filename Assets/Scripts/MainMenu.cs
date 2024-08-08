using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameObject controlScreen;
    public GameObject creditScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    { 
        SceneManager.LoadScene("A1S1 Redesign");
    }

    public void Controls()
    {
        controlScreen.SetActive(true);
    }

    public void BackFromControls()
    {
        controlScreen.SetActive(false);
    }

    public void Credits()
    {
        creditScreen.SetActive(true);
    }

    public void BackFromCredits()
    {
        creditScreen.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }

}
