using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            if(collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            }

            else
            {
                cam.MoveToNewRoom(previousRoom);  
                previousRoom.GetComponent<Room>().ActivateRoom(true); 
                nextRoom.GetComponent<Room>().ActivateRoom(false);

            } 
        }
    }
}
