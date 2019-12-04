using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCheck : MonoBehaviour
{
    public bool IsOnTop;

    public LayerMask WhatIsEdge;
    void Update()
    {
        IsOnTop = false;
        Collider2D[] htiCollider = Physics2D.OverlapCircleAll(gameObject.transform.position,.5f,WhatIsEdge );
        for (var i = 0; i < htiCollider.Length; i++)
        {
            if (htiCollider[i].gameObject != gameObject)
            {
                IsOnTop = true;
            }
           
        }
    }
}
