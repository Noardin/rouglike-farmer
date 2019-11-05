using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Mrkev : Enemy
{
    public float terrorRadius = 5f;
    private bool _playerDetected;
    private bool _wasDetected;
    public Animator animator;
     protected override void Awake()
    {
        base.Awake();
        if (enemybody == null)
        {
            enemybody = gameObject.GetComponent<Rigidbody2D>();
        }

        
    }

     protected override void Start()
     {
         base.Start();
         Debug.Log("Start");
     }


    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _wasDetected = _playerDetected;
        _playerDetected = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, terrorRadius, whatisPlayer);
        foreach (Collider2D collider in hits)
        {
            if (collider != null)
            {
                _playerDetected = true;
                if (!_wasDetected)
                {
                    animator.SetBool("playerDetected",true);
                }
                
            }
        }

        if (!_playerDetected)
        {
            animator.SetBool("playerDetected",false);
        }
        
    }
}
