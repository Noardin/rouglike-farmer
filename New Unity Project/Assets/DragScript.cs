using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
   private bool selected;
   public Transform map;
   private Vector2 lastmousePos;
   public DropdownMaps dropdown;

   private void Start()
   {
      lastmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   }

   private void OnMouseOver()
   {
      if (Input.GetMouseButtonDown(0)& dropdown.dropdown.options.Count !=0)
      {
        
         selected = true;
         Debug.Log("dragged "+ dropdown.dropdown.options.Count);
      }
     
   }

   private void Update()
   {
      if (selected)
      {
         Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         map.position = new Vector2(map.position.x + (cursorPos.x-lastmousePos.x),map.position.y + (cursorPos.y-lastmousePos.y));
         
      }

      if (Input.GetMouseButtonUp(0))
      {
         selected = false;
      }
      lastmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      
   }
}
