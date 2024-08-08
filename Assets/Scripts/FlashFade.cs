using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FlashFade : MonoBehaviour
{
    public Image subject;

    // Start is called before the first frame update
    void Start()
    {
        subject = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        subject.CrossFadeAlpha(0, 0.3f, false);
    }
}
