using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EventTriggerSystem : MonoBehaviour
{
   public PopUps popUps;
   private bool WaitingForTrigger;
   private KeyCode KeyToPress;
   private PopUps.PopUpTypes PopUpType;
   private UnityEvent EventToTrigger;
 

   public enum button
   {
      ENTER, ESC, SPACE
   }

  
   KeyCode ButtonToKeyCode(button button)
   {
      switch (button)
      {
         case button.ESC:
            return KeyCode.Escape;
         case button.ENTER:
            return KeyCode.Return;
         case button.SPACE:
            return KeyCode.Space;
      }

      return KeyCode.None;
   }

   PopUps.PopUpTypes ButtonToPopUpTypes(button button)
   {
      switch (button)
      {
         case button.ESC:
            return PopUps.PopUpTypes.ESC;
         case button.ENTER:
            return PopUps.PopUpTypes.ENTER;
         case button.SPACE:
            return PopUps.PopUpTypes.SPACE;
         default:
            return PopUps.PopUpTypes.ENTER;
      }
   }
   
   public void TriggerByButton(button button, UnityEvent EventToTrigger)
   {
      KeyToPress = ButtonToKeyCode(button);
      if (KeyToPress != KeyCode.None)
      {
         
         PopUpType = ButtonToPopUpTypes(button);
         popUps.ShowPopUp(PopUpType);
         this.EventToTrigger = EventToTrigger;
         WaitingForTrigger = true;
      }
      
   }

   public void CancelTriggerByButton()
   {
      WaitingForTrigger = false;
      KeyToPress = KeyCode.None;
      EventToTrigger = null;
      popUps.HidePopUp();
   }
   private void Update()
   {
      if (WaitingForTrigger)
      {
         if (Input.GetKeyDown(KeyToPress))
         {
            popUps.HidePopUp();
            EventToTrigger.Invoke();
            EventToTrigger = null;
            WaitingForTrigger = false;

         }
      }

      
   }
}
