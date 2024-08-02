using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinChunkGen : MonoBehaviour
{
    public GameObject ruinChunk1;
    public GameObject ruinChunk2;
    public GameObject ruinChunk3;

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
            ruinChunk1.SetActive(true);
        }
        if (chunk == 2)
        {
            ruinChunk2.SetActive(true);
        }
        if (chunk == 3)
        {
            ruinChunk3.SetActive(true);
        }
    }
}
