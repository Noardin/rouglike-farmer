using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public enum PickUpTypes
    {
        Hearth,
        
    }

    public PickUpTypes pickUpType;

    protected virtual void Start()
    {
        
    }

    protected virtual void CollectPickUp()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered");
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            CollectPickUp();
        }
    }

}
