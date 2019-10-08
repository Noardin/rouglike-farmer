using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeHook : MonoBehaviour
{
    public bool IsEdge;
    public LayerMask WhatIsEdge;
    private void Update()
    {
        Collider2D[] htiCollider = Physics2D.OverlapCircleAll(gameObject.transform.position,.2f,WhatIsEdge );
        for (var i = 0; i < htiCollider.Length; i++)
        {
            if (htiCollider[i] != null)
            {
                Debug.Log("IsEdge");
                IsEdge = true;
            }
            else
            {
                Debug.Log("IsNotEdge");
                IsEdge = false;
            }
            Debug.Log("IsEdge"+ IsEdge);
        }
        
    }
}
