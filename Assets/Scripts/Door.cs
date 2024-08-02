using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    //private GameObject door;

    /*
    [SerializeField]
    GameObject exitDoor;
    */
    [SerializeField]
    GameObject[] switches;

    public GameObject BigDoor;
    int noOfSwitches = 0;
    public bool canDestroyText;

    [SerializeField]
    Text switchText;
    public GameObject SwitchCount;
    public int Time;

    void Start()
    {
        GetNoOfSwitches();
        canDestroyText = true;
        //door = GameObject.FindWithTag("Door");
    }

    public int GetNoOfSwitches()
    {
        int x = 0;

        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].GetComponent<Switch>().isOn == false)
                x++;
            else if (switches[i].GetComponent<Switch>().isOn == true)
                noOfSwitches--;
        }

        noOfSwitches = x;
        

        return noOfSwitches;
    }

    public void GetExitDoorState()
    {
        if (noOfSwitches <= 0 && canDestroyText)
        {
            //Opens door / destroy door
            //Camera movement
            Destroy(BigDoor, 1);
            switchText.text = "Door is open";
            StartCoroutine(DestroyText());
            Debug.Log("smd bitch");           
        }
    }
            

    void Update()
    {
        switchText.text = GetNoOfSwitches().ToString();
        GetExitDoorState();
    }

    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(Time);
        SwitchCount.SetActive(false);
        canDestroyText = false; 
    }
}
