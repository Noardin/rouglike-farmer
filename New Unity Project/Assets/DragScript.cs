using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
   private bool selected;
   public Transform map;
   private Vector2 lastmousePos;
   public DropdownMaps dropdown;
   public BoxCollider2D col;

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
      if (selected & CanMove())
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
   private bool CanMove()
   {
      RaycastHit2D ycollision = Physics2D.Raycast(map.position, Vector2.up, col.size.y/2,8 );
      RaycastHit2D xcollision = Physics2D.Raycast(map.position, Vector2.right, col.size.x/2,8);
      Debug.Log("col " + xcollision.collider);
      return (ycollision.collider == null & xcollision.collider == null);
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.cyan;
      Gizmos.DrawRay(map.position, new Vector2(0, col.bounds.size.y/2));
   }
}
