using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelDoor : MonoBehaviour
{
    public EventTriggerSystem EventTriggerSystem;

   
    private player _player;
    private UnityEvent EventToTrigger = new UnityEvent();
    private Transform LeavePosition;
    private CameraFollow cameraController;

    private void Awake()
    {
        EventToTrigger.AddListener(GoToLeavePosition);
        _player = GameObject.Find("Player").GetComponent<player>();
        LeavePosition = GameObject.Find("LeavePosition").transform;
        cameraController = GameObject.Find("CM vcam1").GetComponent<CameraFollow>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            EventTriggerSystem.TriggerByButton(EventTriggerSystem.button.ENTER, EventToTrigger);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("EventTrigger"))
            {
              EventTriggerSystem.CancelTriggerByButton();
            }           
    }

    private void GoToLeavePosition()
    {
        cameraController.StopFollow();
        _player.GoTo(LeavePosition.position, 10);
    }




}
