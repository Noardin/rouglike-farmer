using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class checkpoint : MonoBehaviour
{
    private player player;
    public EventTriggerSystem TriggerSystem;
    public UniqueId UniqueId;
    public bool isSet;
    public bool isActive;

    private UnityEvent TriggerEvent = new UnityEvent();
    // Start is called before the first frame update

    void Awake()
    {
        UniqueId = gameObject.GetComponent<UniqueId>();
        TriggerEvent.AddListener(SetCheckpoint);
        player = GameObject.Find("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("checkpoint");
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
            checkpointController.AcitivateCheckpoint(this);
        }

        if (!isSet)
        {
            checkpointController.SetCheckpointAndUnsetLast(this);
            player.healthManager.HealFull();
            SaveSystem.SavePlayer(player);
        }
        
    }

    public void DeactivateCheckpoint()
    {

        if (isSet)
        {
            checkpointController.DeactivateLastCheckpoint();
        }
        else
        {
            checkpointController.removeCheckpoint(this);
        }


    }
}
