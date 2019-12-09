using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class checkpoint : MonoBehaviour
{
    public EventTriggerSystem TriggerSystem;

    private UnityEvent TriggerEvent = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        TriggerEvent.AddListener(SetCheckpoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            TriggerSystem.TriggerByButton(EventTriggerSystem.button.ENTER, TriggerEvent);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            TriggerSystem.CancelTriggerByButton();
        }
        
    }

    void SetCheckpoint()
    {
        
    }
}
