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
    [HideInInspector]public UniqueId UniqueId;
    
    [HideInInspector]public bool isSet;
    [HideInInspector]public bool isActive;
    [HideInInspector] public bool isDisabled;
    // Start is called before the first frame update

    void Awake()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<player>();
    }

    public void SetID(UniqueId uniqueId)
    {
        UniqueId = uniqueId;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((!isActive | !isSet)& !isDisabled)
        {
            if (other.gameObject.CompareTag("EventTrigger"))
            {
               SetCheckpoint();
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
        ParticleSystem.ColorOverLifetimeModule colorModule = ParticleSystem.colorOverLifetime;
        colorModule.enabled = true;
        colorModule.color = new ParticleSystem.MinMaxGradient(Color.black, Color.gray);
    }
}
