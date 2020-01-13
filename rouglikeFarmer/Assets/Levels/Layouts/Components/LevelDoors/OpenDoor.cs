using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator animator;
    public float timer;
    private float countdown;
    private bool isUp;
    private bool locked;

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
                animator.SetTrigger("Open");
                countdown = timer;
                isUp = true;
            }
        }
        
    }

    private void Update()
    {
        if (isUp)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("Close");
                isUp = false;
                locked = true;
            }
        }
        
    }
}
