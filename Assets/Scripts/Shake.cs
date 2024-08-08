using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shake : MonoBehaviour
{
    public GameObject Text;

    public float CD;
    public float strength;

    void Start()
    {
        StartCoroutine(Shaking());        
    }
    // Update is called once per frame
    void Update()
    {
        if (Text.activeSelf)
        {
            StartCoroutine(Shaking());        
        }
    }

    IEnumerator Shaking()
    {       
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                transform.position = startPosition + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.position = startPosition;
            yield return new WaitForSeconds(CD);
        
    }
}
