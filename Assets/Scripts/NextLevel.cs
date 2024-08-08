using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void A1S1Redesign()
    {
        SceneManager.LoadScene("A1S1Redesign");
    }

    public void A1S1()
    {
        SceneManager.LoadScene("A1S1");
    }

    public void A1S2()
    {
        SceneManager.LoadScene("A1S2");
    } 

    public void Sandstorm()
    {
        SceneManager.LoadScene("SandstormCut");
    }

}
