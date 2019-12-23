using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitboxcheck : MonoBehaviour
{


    private Animator animator;
    public Rigidbody2D playerBody;
    private bool isBeeingHit = false;
    private float hitTimer = 0f;
    public float hitDelay = .2f;
    private Transform EnemyTransform;
    private Vector3 MoveDirection;
    private player playerScript;
    public bool Invincible;
    private CharacterController2D controller;
    public float knockbackForce = 0f;
    [Range(0,-1)]public float knockupForce = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponentInParent(typeof(player)) as player;
        playerBody = GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
        animator = GetComponentInParent(typeof(Animator)) as Animator;
        controller = GetComponentInParent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeeingHit)
        {
            hitTimer += Time.deltaTime;
        }
        if(hitTimer >= hitDelay)
        {
            isBeeingHit = false;
            animator.SetBool("IsHit", false);
            hitTimer = 0f;
        }
        
    }

    public IEnumerator InvincibleEn(float seconds)
    {
        Invincible = true;
        yield return new WaitForSeconds(seconds);
        Invincible = false;

    }

    public void ImInvincible(float seconds)
    {
        StartCoroutine(InvincibleEn(seconds));
    }

    public void HitPlayer(Enemy col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") && isBeeingHit == false && !Invincible)
        {
            Debug.Log("hitboxcheck");
            EnemyTransform = col.gameObject.GetComponentInParent<Rigidbody2D>().transform;
            MoveDirection = EnemyTransform.position - playerBody.transform.position;
            MoveDirection.y = knockupForce;
            MoveDirection.z = 0f;
            Enemy enemy = col.GetComponentInParent<Enemy>();
            double damage = enemy.DmgDeal;
            
            playerBody.AddForce(MoveDirection.normalized * -200f*knockbackForce);
            isBeeingHit = true;
            animator.SetBool("IsHit", true);

            playerScript.TakeDamage(damage);
        }
       
    }
}
