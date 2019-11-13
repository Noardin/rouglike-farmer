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
        Debug.Log("Start");
    }

    protected override void Idling()
     {
         if (idlingState == IdlingState.Hidden)
         {
             Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, AgroRange, whatisPlayer);
             foreach (Collider2D collision in hits)
             {
                 if (collision != null)
                 {
                     animator.ResetTrigger("Hide");
                     idlingState = IdlingState.Popped;
                     State = EnemyState.Attacking;
                            
                 }
             }
            
         }
         if(idlingState == IdlingState.Popped)
         {
             // hides after x seconds...
             if (hideTimer >= hideDelay)
             {
                        
                 hideTimer = 0f;
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(8f, 7f), 0f, whatisPlayer);
        foreach (Collider2D col in colliders)
        {
            if (col != null)
            {
                col.gameObject.GetComponentInParent<player>().TakeDamage(DmgDeal);
            }
        }
    }

    protected override void Attacking()
    {
        if (!isAttacking)
        {
            if (AttackTimer >= AttackDelay)
            {
                animator.ResetTrigger("Preparing");
                animator.SetTrigger("Attacking");
            }
            else
            {
                if (!isPreparing)
                {
                    isPreparing = true;
                    animator.SetTrigger("Preparing");
                }
                AttackTimer += Time.deltaTime;
                Debug.Log("Preparing To Attack");
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
