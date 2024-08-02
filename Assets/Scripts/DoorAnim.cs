using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    Animator anim;

    //tracks the state of door
    [SerializeField]
    GameObject DoorType;

    public int stateOfDoor = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        /*
        //Set door entry to open
        if (DoorType.name == "EntryDoor")
            OpenDoor();
        
        //set exit door to closed
        if (DoorType.name == "ExitDoor")
            LockDoor();
        */
    }

    //Function to lock / close the door and set its state
    //(No need use unless for whatever reason you want to close back the door)
    void LockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            //Sets animation of open door
            //anim.setFloat("DoorState", 1);
            stateOfDoor = 1;
        }
    }

    //Function to unlock the door and set its state
    void UnlockDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            //Sets animation of open door
            Destroy(gameObject);
            //anim.setFloat("DoorState", 2);
            stateOfDoor = 2;
        }
    }

    //Function to open the door and set its state
    //(No use of this function)
    void OpenDoor()
    {
        if (DoorType.name == "ExitDoor")
        {
            //Sets animation of open door
            //anim.setFloat("DoorState", 3);
            stateOfDoor = 3;
        }
    }
    //Function to set the door state
    public void SetDoorState(int state)
    {
        if (state == 1)
            LockDoor();
        if (state == 2)
            UnlockDoor();
        if (state == 3)
            OpenDoor();
    }

    /*
    //Function to set the door state
    public void SetDoorState(int state)
    {
        if (state == 1 && DoorType.name == "ExitDoor")
            LockDoor();
        if (state == 2 && DoorType.name == "ExitDoor")
            UnlockDoor();
        if (state == 3 && DoorType.name == "ExitDoor")
            OpenDoor();
    }
    */

    //Function to get the current door state
    public int GetDoorState()
    {
        return stateOfDoor;
    }

}
