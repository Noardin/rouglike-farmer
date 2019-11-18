using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
    public Animator animator;


	public float runSpeed = 40f;
  
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    private bool dash = false;
    public float slideDelay;
    private float SlideTimer;
    

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused)
        {
            if (SlideTimer > 0)
                {
                    SlideTimer -= Time.deltaTime;
                } 
                if (!animator.GetBool("IsClimbing")){
                
                    if (!animator.GetBool("IsAttacking"))
                    {
                        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
                    }
                    else
                    {
                        horizontalMove = 0f;
                    }
        
                    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
                    if (Input.GetButtonDown("Jump"))
                    {
                        jump = true;
                        animator.SetBool("IsJumping", true);
                    }
        
                    if (Input.GetButtonDown("Crouch"))
                    {
                        crouch = true;
                    }
                    else if (Input.GetButtonUp("Crouch"))
                    {
                        crouch = false;
                    }
        
                    if (Input.GetButtonDown("Fire2") && !animator.GetBool("LedgeHooking") && !animator.GetBool("IsSliding") &&
                        SlideTimer <= 0)
                    {
                        SlideTimer = slideDelay;
                        dash = true;
                        animator.SetBool("IsSliding", true);
                        Debug.Log("slide");
                    }
                }
        }
        
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void IsCrouching(bool iscrouching)
    {
        animator.SetBool("IsCrouching", iscrouching);
    }

	void FixedUpdate ()
	{
		// Move our character
        if (!animator.GetBool("IsClimbing"))
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        }
		
        dash = false;
		jump = false;
	}
}
