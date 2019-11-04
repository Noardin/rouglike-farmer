using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private int currentAttack;

    private bool isAttacking;
    public Transform AttackPoint;
    public float AttackRadius = 10f;
    public ContactFilter2D whatisenemy;
    public Animator animator;
    private Collider2D[] enemiesHit = new Collider2D[5];
    private Collider2D attackCollider;
    public shockwaveSpawner shockWaveSpawner;
    public Transform Feetpos;

    public CameraShake camshake;

    public shockwaveSpawner _shockWaveSpawner;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetBool("IsAttacking", true);
                Attackfun();
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetInteger("Attack", 0);
            currentAttack = 0;
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
        }

    }

    public void onAttackEnd()
    {
        isAttacking = false;
        
        animator.SetBool("IsAttacking", false);
    }
    public void onAttackStart()
    {
        isAttacking = true;
        Debug.Log("attack has started");
        animator.SetBool("IsAttacking", true);
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
                Debug.Log("dropdownDeal");
                colliders[i].GetComponent<Enemy>().TakeDamage(10);
            }
            else
            {
                Debug.Log("missingEnemyScript");
            }
        }
    }

    void Attackfun()
    {
        if (currentAttack <= 3)
        {
            Debug.Log(currentAttack);
            currentAttack += 1;
        }
        else
        {
            Debug.Log("again");
            currentAttack = 0;

        }
        animator.SetInteger("Attack", currentAttack);

    }
}