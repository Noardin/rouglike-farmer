using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class Cow : Enemy
{
   private bool isCharging;
   public float ChargeDistance;
   
   
      
   

   protected override void Attack()
   {
      base.Attack();
      

   }

   public override void DealDmg()
   {
      base.DealDmg();
      isCharging = false;
      enemybody.velocity = Vector2.zero;
   }

   public void StartCharging()
   {
      isCharging = true;
   }

   protected override void Update()
   {
      base.Update();
      if (isCharging)
      {
         Vector2 targetVelocity = new Vector2(moveDirection.x*15f*Time.fixedDeltaTime*5f*ChargeDistance, enemybody.velocity.y);
         enemybody.velocity = Vector3.SmoothDamp(enemybody.velocity, targetVelocity, ref velocity, movementSmoothing);
         Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(attackPosition.position, 2f, whatisPlayer);
         foreach (var coll in collider2Ds)
         {
            if (coll.CompareTag("EventTrigger"))
            {
               animator.Play("ovce_attack", 0, 151f/167f);
               
            }
         }
         
      }
      
   }
}
