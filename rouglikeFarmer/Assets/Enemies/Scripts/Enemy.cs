using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Enemy: MonoBehaviour
{
    public int HP = 100;
    private GameObject player;
    private Rigidbody2D playerbody;
    private Vector3 moveDirection = new Vector3(1f,0f,0);
    public Rigidbody2D enemybody;
    public float KnockBackFoce = 10f;
    public LayerMask whatisPlayer;
    public GameObject particleSystem;
    public Color HurtParticleColor;
    public Animator animator;
    public float AgroRange = 3f;
    public float AttackDelay = 0.5f;
    public float idleMoveSpeed = 10f;
    public float attackMoveSpeed = 20f;
    public LayerMask WhatisGround;
    public Transform attackPosition;
    public Transform feetPosition;
    public float movementSmoothing;
    public float moveDistance;
    private float moveCurrentDistance;
    public float moveWait;
    private float currentMoveWait;
    private Vector3 velocity = Vector3.zero;
    protected bool isAttacking;
    public float looseAgroRange = 5f;
    protected bool isPreparing;
    public double DmgDeal;
    protected bool undamagable;
    public float attackRange = 1f;
    protected float AttackTimer;
    public PopUps popUps;
    protected EnemyState State = EnemyState.Idling;
    public SpriteRenderer SR;
    protected enum EnemyState
    {
        Stunned, Attacking, Idling
    }
    protected virtual void Awake()
    {
        if (enemybody == null)
        {
            enemybody = gameObject.GetComponent<Rigidbody2D>();
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        switch (State)
        {
            case EnemyState.Idling:
                Idling();
                break;
            
            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }

    protected virtual void Idling()
    {
        if (IsInRange(AgroRange, whatisPlayer))
        {
            State = EnemyState.Attacking;
        }
        
        Move(idleMoveSpeed);
    }

    protected bool IsInRange(float range, LayerMask whatToLookFor)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, whatToLookFor);
        foreach (Collider2D collision in hits)
        {
            if (collision.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }
    protected virtual void Attacking()
    {
        if (!IsInRange(looseAgroRange, whatisPlayer))
        {
            isAttacking = false;
            isPreparing = false;
            AttackTimer = 0;
            State = EnemyState.Idling;
            animator.ResetTrigger("Attacking");
            animator.SetTrigger("Idling");
            return;
        }
        if (!FacingPlayer())
        {
            Flip();
        }
        if (PlayerInAttackRange())
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
                        Debug.Log("preparing");
                        popUps.PopUpTimed(PopUps.PopUpTypes.Exclemation, AttackDelay,1.2f, 1.1f );
                        animator.SetTrigger("Preparing");
                    }
                    AttackTimer += Time.deltaTime;
                }
            }
        }
        else
        {
            Move(attackMoveSpeed);
        }
        
    }

    protected virtual void Move(float moveSpeed)
    {
        if (moveCurrentDistance >= moveDistance)
        {
            moveCurrentDistance = 0;
            currentMoveWait = moveWait;
            Flip();
            
        }
        if (!CanMove())
        {
          Flip();   
        }

        if (currentMoveWait <= 0)
        {
            moveCurrentDistance += 10f * Time.deltaTime;
            Vector2 targetVelocity = new Vector2(moveDirection.x*moveSpeed*Time.fixedDeltaTime*5f*idleMoveSpeed, enemybody.velocity.y);
            enemybody.velocity = Vector3.SmoothDamp(enemybody.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
        else
        {
            currentMoveWait -= Time.deltaTime;
        }
        
        
        
        
    }

    protected virtual bool CanMove()
    {
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, moveDirection, 1f, WhatisGround);
        return hit.collider == null;
    }

    protected bool PlayerInAttackRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPosition.position, moveDirection, attackRange, whatisPlayer);
        return hit.collider != null;
    }

    protected bool FacingPlayer()
    {
        
        Vector3 playerDirection = player.transform.position - gameObject.transform.position;
        
        return Vector3.Dot(playerDirection, moveDirection) > 0;
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual IEnumerator Stun(float duration)
    {

        yield return new WaitForSeconds(duration);
    }
    
    public virtual void OnAttackEnd()
    {
        isAttacking = false;
        State = EnemyState.Idling;
        animator.ResetTrigger("Attacking");
        animator.SetTrigger("Idling");
        
    }

    public virtual void DealDmg()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition.position, 3f, whatisPlayer);
        foreach (Collider2D col in colliders)
        {
            if (col != null && col.CompareTag("PlayerHitZone"))
            {
                col.gameObject.GetComponent<Hitboxcheck>().HitPlayer(this);
            }
        }
    }
    public void OnAttackStart()
    {
        isPreparing = false;
        AttackTimer = 0;
        isAttacking = true;
    }
    protected virtual void Die()
    {
        animator.SetTrigger("IsDying");
        Destroy(gameObject);
    }

    protected void Flip()
    {
        moveDirection.x *= -1;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (!undamagable)
        {
            GameObject instanceParticle = Instantiate(particleSystem,transform.position,Quaternion.identity);
            ParticleSystem.MainModule mainModule = instanceParticle.GetComponent<ParticleSystem>().main;
            mainModule.startColor = HurtParticleColor;
//        Debug.Log("player", player);
//        playerbody = player.GetComponent<Rigidbody2D>();
//        moveDirection = enemybody.transform.position - playerbody.transform.position;
//        Debug.Log("dircion"+moveDirection.normalized);
            HP -= damage;
//        float xForce;
//        if (moveDirection.x > 0)
//        {
//             xForce = 1 -moveDirection.normalized.x;
//        }
//        else
//        {
//            xForce = -1 -moveDirection.normalized.x;
//        }
//        Debug.Log("force "+ xForce);
//       
//
//        GetComponent<Rigidbody2D>().AddForce(new Vector3(xForce*KnockBackFoce, 5, 0),ForceMode2D.Impulse);
        
            if(HP <= 0)
            {
                Die();

            }  
        }
        

    }
}
