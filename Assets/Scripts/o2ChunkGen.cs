using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o2ChunkGen : MonoBehaviour
{
    public GameObject o2Chunk1;
    public GameObject o2Chunk2;
    public GameObject o2Chunk3;

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
            o2Chunk1.SetActive(true);
        }
        if (chunk == 2)
        {
            o2Chunk2.SetActive(true);
        }
        if (chunk == 3)
        {
            o2Chunk3.SetActive(true);
        }
    }
}
