using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelDoor : MonoBehaviour
{
    public EventTriggerSystem EventTriggerSystem;

    public mainSceneController.Levels ToLevel;
    private UnityEvent EventToTrigger = new UnityEvent();

    private void Awake()
    {
        EventToTrigger.AddListener(GoToLVL);
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

    public void GoToLVL()
    {
        mainSceneController.GoToLevel(ToLevel);
    }


}
