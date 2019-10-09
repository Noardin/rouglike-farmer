using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LedgeHook : MonoBehaviour
{
    public bool IsEdge;
    public LayerMask WhatIsEdge;
    private void FixedUpdate()
    {
        IsEdge = false;
        Collider2D[] htiCollider = Physics2D.OverlapCircleAll(gameObject.transform.position,.2f,WhatIsEdge );
        for (var i = 0; i < htiCollider.Length; i++)
        {
            if (htiCollider[i].gameObject != gameObject)
            {
                Debug.Log("IsEdge");
                IsEdge = true;
            }
           
        }
        
        
    }
}
