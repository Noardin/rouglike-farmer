using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Mrkev : Enemy
{
    public float hideDelay = 3f;
    private float hideTimer;
    private IdlingState idlingState;
   
    private enum IdlingState
    {
        Popped, Hidden
    }

    protected override void Start()
    {
        base.Start();
        undamagable = true;
   
    }

    protected override void Idling()
     {
         if (idlingState == IdlingState.Hidden)
         {
             if (IsInRange(AgroRange, whatisPlayer))
             {
                 animator.ResetTrigger("Hide");
                 idlingState = IdlingState.Popped;
                 State = EnemyState.Attacking;
                 
             }
         }
         if(idlingState == IdlingState.Popped)
         {
             // hides after x seconds...
             if (hideTimer >= hideDelay)
             {
                        
                 hideTimer = 0f;
                 undamagable = true;
                 animator.ResetTrigger("Idling");
                 animator.SetTrigger("Hide");
                 idlingState = IdlingState.Hidden;
             }
             else{
                 hideTimer += Time.deltaTime;
             }
         }
     }

    public override void DealDmg()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(4f, 1f), 0f, whatisPlayer);
        
        foreach (Collider2D col in colliders)
        {
            if (col != null && col.CompareTag("PlayerHitZone"))
            {
                col.gameObject.GetComponent<Hitboxcheck>().HitPlayer(this);
            }
        }
    }

    protected override void Attacking()
    {
        if (!isAttacking)
        {
            if (AttackTimer >= AttackDelay)
            {
                undamagable = false;
                animator.ResetTrigger("Preparing");
                animator.SetTrigger("Attacking");
            }
            else
            {
                if (!isPreparing)
                {
                    isPreparing = true;
                    popUps.PopUpTimed(PopUps.PopUpTypes.Exclemation, AttackDelay,1.8f, 1.1f );
                    animator.SetTrigger("Preparing");
                }
                AttackTimer += Time.deltaTime;
             
            }
        }
    }

    public override void OnAttackEnd()
     {
         base.OnAttackEnd();
         
     }
     // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        
        
        
    }
}
