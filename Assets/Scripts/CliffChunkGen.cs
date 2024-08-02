using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffChunkGen : MonoBehaviour
{
    public GameObject cliffChunk1;
    public GameObject cliffChunk2;
    public GameObject cliffChunk3;

    public int chunk;

    // Start is called before the first frame update
    void Start()
    {
        chunk = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (chunk == 1)
        {
            cliffChunk1.SetActive(true);
        }
        if (chunk == 2)
        {
            cliffChunk2.SetActive(true);
        }
        if (chunk == 3)
        {
            cliffChunk3.SetActive(true);
        }
    }
}
