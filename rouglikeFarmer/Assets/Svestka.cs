using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Svestka : Enemy
{
    private Transform playerT;
    public float JumpHeight = 40;
    private float gravity;

    protected override void Start()
    {
        base.Start();
        playerT = GameObject.Find("Player").transform;
        gravity = Physics.gravity.y*enemybody.gravityScale;
    }
    
    public void Jumping()
    {
        enemybody.velocity = calculateJumpVelocity();
    }

    Vector3 calculateJumpVelocity()
    {
        float DistanceX = playerT.position.x - transform.position.x;
        float DistanceY = playerT.position.y - transform.position.y;
        float finalHeight = JumpHeight;
        if (DistanceY > finalHeight)
        {
            finalHeight = DistanceY;
        }
        Vector3 DistanceXZ = new Vector3(DistanceX, 0, 0);
        Vector3 VelocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * finalHeight);
        Vector3 VeloctyXZ = DistanceXZ /
                            (Mathf.Sqrt(-2 * finalHeight / gravity) +
                             Mathf.Sqrt(2 * (DistanceY - JumpHeight) / gravity));
        return VeloctyXZ + VelocityY;


    }

   
}
