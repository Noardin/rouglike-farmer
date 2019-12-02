using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public EventTriggerSystem TriggerSystem;
    public GameObject Dialog;
    public GameObject DialogCanvas;
    public Transform DialogWindowTransform;
    private GameObject DialogWindow;
    public PopUps PopUps;
    private UnityEvent TriggerByButtonEvent = new UnityEvent();
     private void Start()
    {
        TriggerByButtonEvent.AddListener(OnTriggerEventFunction);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            Debug.Log("entered");
            TriggerSystem.TriggerByButton(EventTriggerSystem.button.ENTER, TriggerByButtonEvent);
            
        }
        
    }

    protected void StartDialog(string text)
    {
        
        DialogWindow = Instantiate(Dialog, DialogWindowTransform);
        DialogWindow.transform.parent = DialogCanvas.transform;
        Dialog dialog = DialogWindow.GetComponent<Dialog>();
        dialog.Say(text, false);
    }

    protected void LeaveDialog()
    {
        if (DialogWindow != null)
        {
            Dialog dialog = DialogWindow.GetComponent<Dialog>();
            dialog.LeaveDialog();
        }
        
    }

    public void OnTriggerEventFunction()
    {
        Debug.Log("triggerFunction");
        StartDialog("ahoj");
    }

   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            TriggerSystem.CancelTriggerByButton();
            LeaveDialog();
            Debug.Log("exited");
        }
        
    }
}
