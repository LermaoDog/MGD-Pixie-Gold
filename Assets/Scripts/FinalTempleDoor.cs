using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTempleDoor : MonoBehaviour
{
    public GameObject Door;
    public bool plateHe = false;
    public bool plateQi = false;
    public bool plateTian = false;

    void Update()
    {
        if (plateHe == true && plateQi == true && plateTian == true)
        {
            Door.SetActive(false);
        }
    }
}
