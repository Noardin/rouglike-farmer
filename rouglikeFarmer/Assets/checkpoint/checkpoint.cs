using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class checkpoint : MonoBehaviour
{
    public player player;
    public EventTriggerSystem TriggerSystem;
    public string Id;
    public bool isSet;
    public bool isActive;

    private UnityEvent TriggerEvent = new UnityEvent();
    // Start is called before the first frame update

    void Awake()
    {
        Id = transform.position.x + "_" + transform.position.y + "_" + transform.position.z;
        TriggerEvent.AddListener(SetCheckpoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
        {
            if (other.gameObject.CompareTag("EventTrigger"))
                    {
                        TriggerSystem.TriggerByButton(EventTriggerSystem.button.ENTER, TriggerEvent);
                    }
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isActive)
        {
            if (other.gameObject.CompareTag("EventTrigger"))
                    {
                        TriggerSystem.CancelTriggerByButton();
                    }
        }
        
        
    }


    public void SetCheckpoint()
    {
        if (!isActive)
        {
            isSet = true;
            isActive = true;
            checkpointController.SetCheckpoint(this);
            player.healthManager.HealFull();
            SaveSystem.SavePlayer(player);
            Debug.Log("Checkpoint Set");
        }    
    }

    public void DestroyCheckpoint()
    {

        if (isSet)
        {
            checkpointController.UnsetLastCheckpoint();
        }
        else
        {
            checkpointController.removeCheckpoint(this);
        }
        
        Destroy(gameObject);
        
    }
}
