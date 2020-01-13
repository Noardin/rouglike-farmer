using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private Animator animator;
    private bool locked = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!locked)
        {
            if (other.CompareTag("EventTrigger"))
            {
                animator.SetTrigger("Close");
            }
        }
        
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
        locked = false;
    }
}
