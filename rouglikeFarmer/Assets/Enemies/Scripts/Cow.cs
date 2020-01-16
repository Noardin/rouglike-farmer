using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class Cow : Enemy
{
   private bool isCharging;
   protected override void OnTriggerEnter2D(Collider2D other)
   {
      base.OnTriggerEnter2D(other);
    
      
      if (other.gameObject.CompareTag("EventTrigger") & isAttacking & isCharging)
      {
       
         animator.Play("ovce_attack", 0, 151f/167f);
      }
   }

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
         Vector2 targetVelocity = new Vector2(moveDirection.x*15f*Time.fixedDeltaTime*5f*idleMoveSpeed, enemybody.velocity.y);
         enemybody.velocity = Vector3.SmoothDamp(enemybody.velocity, targetVelocity, ref velocity, movementSmoothing);
      }
      
   }
}
