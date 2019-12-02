using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGroundVisibilityToCamera : MonoBehaviour
{
    private SpriteRenderer SR;
    public float FadeSpeed = 0.8f;
    public float AlphaToFadeTo = 10f;
    private bool fadeToMax;
    private bool fadeToMin;
    private Color originalColor;
    private GameObject player;
    
    private void Start()
    {
        player = GameObject.Find("Player");
        SR = gameObject.GetComponent<SpriteRenderer>();
        originalColor = SR.color;
    }

    private void Update()
    {
        if (fadeToMin) 
        {
            SR.color = Color.Lerp(SR.color, new Color(0,0, 0,AlphaToFadeTo), FadeSpeed * Time.deltaTime);
        }

        if (fadeToMax)
        {
            SR.color = Color.Lerp(SR.color, originalColor, FadeSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == player.layer)
        {
            fadeToMin = true;
            fadeToMax = false;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == player.layer)
        {
            fadeToMax = true;
            fadeToMin = false;
        }
    }
}
