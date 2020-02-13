using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private int currentAttack;
    private bool ToAttack;
    private bool isAttacking;
    public Transform AttackPoint;
    public float AttackRadius = 10f;
    public ContactFilter2D whatisenemy;
    public Animator animator;
    private Collider2D[] enemiesHit = new Collider2D[5];
    private Collider2D attackCollider;
    public shockwaveSpawner shockWaveSpawner;
    public Transform Feetpos;
    public player _player;
    public CameraShake camshake;

    public shockwaveSpawner _shockWaveSpawner;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused && !_player.PlayerControlledMovementDisabled)
        {
            if (!isAttacking)
            {
                if (ToAttack)
                {
                    animator.SetBool("IsAttacking", true);
                    Attackfun();
                }
            }
            if (isAttacking && !ToAttack)
            {
                animator.SetInteger("Attack", 0);
                currentAttack = 0;
                isAttacking = false;
                animator.SetBool("IsAttacking", false);
            }
        }



    }

    public void onAttackEnd()
    {
        
    }
    public void onAttackStart()
    {
        isAttacking = true;
    
        animator.SetBool("IsAttacking", true);
    }
    public void AttackDown()
    {
        ToAttack = true;
    }

    public void AttackUp()
    {
        ToAttack = false;
        
    }
    public void DealDmg(int AttackNumber)
    {
        camshake.Shake(.1f, 1f, 10f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, whatisenemy.layerMask);
        foreach (var collider in hits)
        {
            if (collider.gameObject != null)
            {
                collider.GetComponent<Enemy>().TakeDamage(10);
            }
        }

    }

    public void DropDownAttack()
    {
        float MaxExpand = shockWaveSpawner.MaxExpantion;
        camshake.Shake(.3f, 2f, 1f);
        _shockWaveSpawner.Spawn(0.05f, 1f, 0.2f, 3);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Feetpos.transform.position, MaxExpand * 3, whatisenemy.layerMask);
        
        for(var i =0; i < colliders.Length; i++)
        {
            
            if (colliders[i].GetComponent<Enemy>() != null)
            {
                
                colliders[i].GetComponent<Enemy>().TakeDamage(10);
            }
            
        }
    }

    void Attackfun()
    {
        if (currentAttack <= 3)
        {
        
            currentAttack += 1;
        }
        else
        {
          
            currentAttack = 0;

        }
        animator.SetInteger("Attack", currentAttack);

    }
}