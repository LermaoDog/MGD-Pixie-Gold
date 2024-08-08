using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public CinemachineVirtualCamera cineCam;
    public AdvancedSliding slideScript;

    public float startFov;
    public float endFov;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        startFov = cineCam.m_Lens.OrthographicSize;

        if (cineCam.m_Lens.OrthographicSize != endFov)
        {
            StartCoroutine(smoothFov());
        }
    }
    public void IncreaseFov()
    {
        endFov = 18f;
    }
    public void DecreaseFov()
    {
        endFov = 13f;
    }

    IEnumerator smoothFov()
    {
        //float fovDiff = Mathf.Abs(endFov - cineCam.m_Lens.OrthographicSize);
        float time = 0;
        while(time < duration)
        {
            cineCam.m_Lens.OrthographicSize = Mathf.Lerp(startFov, endFov, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
