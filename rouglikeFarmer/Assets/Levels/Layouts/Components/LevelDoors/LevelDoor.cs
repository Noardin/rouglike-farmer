using System;
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
    private CloseDoor closeDoor;
    private GameManagement GM;
    public SpriteRenderer SR;
    public Sprite ActiveSwitch;

    private void Awake()
    {
        EventToTrigger.AddListener(GoToLeavePosition);
        _player = GameObject.Find("Player").GetComponent<player>();
        LeavePosition = GameObject.Find("LeavePosition").transform;
        cameraController = GameObject.Find("CM vcam1").GetComponent<CameraFollow>();
        closeDoor = GameObject.Find("Door_close").GetComponentInChildren<CloseDoor>();
    }

    private void Start()
    {
        GM = GameObject.Find("_GM").GetComponent<GameManagement>();
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
        if (GM.CanFinish())
        {
            SR.sprite = ActiveSwitch;
            cameraController.StopFollow();
            closeDoor.OpenDoor();
            _player.GoTo(LeavePosition.position, 10);
        }

    }




}
