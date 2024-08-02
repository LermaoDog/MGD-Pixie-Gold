using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChunkGen : MonoBehaviour
{
    public GameObject basicChunk1;
    public GameObject basicChunk2;
    public GameObject basicChunk3;
    public GameObject basicChunk4;
    public GameObject basicChunk5;

    public int chunk;

    // Start is called before the first frame update
    void Start()
    {
        chunk = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(chunk == 1)
        {
            basicChunk1.SetActive(true);
        }
        if (chunk == 2)
        {
            basicChunk2.SetActive(true);
        }
        if (chunk == 3)
        {
            basicChunk3.SetActive(true);
        }
        if (chunk == 4)
        {
            basicChunk4.SetActive(true);
        }
        if (chunk == 5)
        {
            basicChunk5.SetActive(true);
        }
    }
}
