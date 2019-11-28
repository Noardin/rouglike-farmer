using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject Dialog;
    public GameObject DialogCanvas;
    public Transform DialogWindowTransform;
    private GameObject DialogWindow;
    public PopUps PopUps;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            PopUps.ShowPopUp(PopUps.PopUpTypes.Enter);
        }
        
    }

    protected void StartDialog(string text)
    {
        DialogWindow = Instantiate(Dialog, DialogWindowTransform);
        DialogWindow.transform.parent = DialogCanvas.transform;
        Dialog dialog = DialogWindow.GetComponent<Dialog>();
        dialog.Say(text);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EventTrigger"))
        {
            PopUps.HidePopUp();
        }
        
    }
}
