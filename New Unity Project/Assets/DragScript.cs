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
   private Camera cam;
   private SpriteRenderer mapSR;
   private Sprite previosSprite;
   public BoxCollider2D mapBorder;
   public float maxX;
   public float maxY;
   public float movedX;
   public float movedY;
   public cursorHandler CursorHandler;
   private void Start()
   {
      cam =Camera.main;
      lastmousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      mapSR = map.GetComponent<SpriteRenderer>();

   }

   private void OnMouseOver()
   {
      
      if (Input.GetMouseButtonDown(0)& dropdown.dropdown.options.Count !=0)
      {
         selected = true;
         Debug.Log("dragged "+ dropdown.dropdown.options.Count);
      }
     
   }

   private void OnMouseEnter()
   {
      CursorHandler.currentCursor = cursorHandler.CursorTypes.drag;
   }

   private void OnMouseExit()
   {
      CursorHandler.currentCursor = cursorHandler.CursorTypes.point;
   }

   private void Update()
   {
      if (previosSprite != mapSR.sprite)
      {
         Bounds spritebounds = mapSR.sprite.bounds;
         float scale = mapSR.transform.localScale.x;
         maxX = spritebounds.extents.x*scale- mapBorder.bounds.size.x/2;
         maxY = spritebounds.extents.y*scale- mapBorder.bounds.size.y/2;
         Debug.Log("spriteX "+spritebounds.extents.x);
         cam.transform.position = new Vector3(0,0,cam.transform.position.z);
         movedX = 0f;
         movedY = 0f;
         previosSprite = mapSR.sprite;
      }   
      if (selected)
      {
         Vector2 cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
         Vector2 moveAmount = new Vector2(-cursorPos.x+lastmousePos.x,  -cursorPos.y+lastmousePos.y);
         Vector3 NextPos = new Vector3((cam.transform.position.x +moveAmount.x),(cam.transform.position.y +moveAmount.y), cam.transform.position.z);
         
         if (CanMove(moveAmount))
         {
            cam.transform.position = NextPos;
            movedX += moveAmount.x;
            movedY += moveAmount.y;
         }
      }

      if (Input.GetMouseButtonUp(0))
      {
         selected = false;
      }
      lastmousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      
   }

   private bool CanMove(Vector2 moveAmount)
   {
      return (movedX + moveAmount.x < maxX & movedX + moveAmount.x > -maxX)& (movedY + moveAmount.y < maxY & movedY + moveAmount.y > -maxY);
   }


}
