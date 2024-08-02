using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToA2S1 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(NextScene());
    }


    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(10.5f);
        SceneManager.LoadScene("A2S1");
    }
}
