using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PickUps : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Transform playerPos;
    public LayerMask PlayerLayer;
    public float flySpeed = 10f;
    private Vector3 m_Velocity = Vector3.zero;
    public float flySmoothing = 0.2f;
    public bool IsFying;
    public enum PickUpTypes
    {
        Hearth,
        
    }

    public PickUpTypes pickUpType;

    protected virtual void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void FlyToPlayer()
    {
        Vector2 targetVelocitu = new Vector2((playerPos.position.x - transform.position.x)*10f*Time.deltaTime*flySpeed, (playerPos.position.y-transform.position.y)*10f*Time.deltaTime*flySpeed);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocitu, ref m_Velocity, flySmoothing);
    }
    protected virtual void CollectPickUp()
    {
        IsFying = false;
        Destroy(gameObject);
    }
    

    private void Update()
    {
        if (IsFying)
        {
            FlyToPlayer();
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, PlayerLayer);
        foreach (Collider2D col in colliders)
        {
            if (col != null)
            {
                Debug.Log("collected");
                CollectPickUp();
            }
        }
    }
}
