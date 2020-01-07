using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class checkpoint : MonoBehaviour
{
    private player player;
    public Sprite AciteCheckpointSprite;
    public Sprite InactiveCheckpointSprite;
    public ParticleSystem ParticleSystem;
    [HideInInspector]public SpriteRenderer SR;
    public EventTriggerSystem TriggerSystem;
    [HideInInspector]public UniqueId UniqueId;
    
    [HideInInspector]public bool isSet;
    [HideInInspector]public bool isActive;

    private UnityEvent TriggerEvent = new UnityEvent();
    // Start is called before the first frame update

    void Awake()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        TriggerEvent.AddListener(SetCheckpoint);
        player = GameObject.Find("Player").GetComponent<player>();
    }

    public void SetID(UniqueId uniqueId)
    {
        UniqueId = uniqueId;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive | !isSet)
        {
            if (other.gameObject.CompareTag("EventTrigger"))
                    {
                        TriggerSystem.TriggerByButton(EventTriggerSystem.button.ENTER, TriggerEvent);
                    }
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isActive | !isSet)
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
            player.healthManager.HealFull();
        }

        if (!isSet)
        {
            checkpointController.SetCheckpointAndUnsetLast(this);
            
            SaveSystem.SavePlayer(player);
        }

        SR.sprite = AciteCheckpointSprite;
        ParticleSystem.Play();

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

        SR.sprite = InactiveCheckpointSprite;
        ParticleSystem.Stop();
    }
}
