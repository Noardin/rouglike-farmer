
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
    public Animator animator;


	public float runSpeed = 40f;
  
	float horizontalMove = 0f;
    private float horizontalDirection;
	bool jump = false;
	bool crouch = false;
    private bool dash = false;
    public float slideDelay;
    private float SlideTimer;
    private player _player;


    private void Awake()
    {
        _player = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused && !_player.PlayerControlledMovementDisabled )
        {
            if (SlideTimer > 0)
                {
                    SlideTimer -= Time.deltaTime;
                } 
                if (!animator.GetBool("IsClimbing")){
                
                    if (!animator.GetBool("IsAttacking"))
                    {
                        horizontalMove = horizontalDirection * runSpeed;

                    }
                    else
                    {
                        horizontalMove = 0f;
                    }
        
                    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
                    
                    if (Input.GetButtonDown("Crouch"))
                    {
                        crouch = true;
                    }
                    else if (Input.GetButtonUp("Crouch"))
                    {
                        crouch = false;
                    }
        
                }
        }
        
    }

    public void MoveRightDown()
    {
        horizontalDirection = 1f;
    }

    public void MoveRightUp()
    {
        horizontalDirection = 0f;
    }

    public void MoveLeftDown()
    {
        horizontalDirection = -1f;
    }

    public void MoveLeftUp()
    {
        horizontalDirection = 0f;
    }

    public void JumpUp()
    {
        if (!PauseMenu.paused && !_player.PlayerControlledMovementDisabled)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        
    }

   

    public void Roll()
    {
        if (!PauseMenu.paused && !_player.PlayerControlledMovementDisabled && !animator.GetBool("LedgeHooking") 
            && !animator.GetBool("IsSliding") && SlideTimer <= 0)
        {
            SlideTimer = slideDelay;
            dash = true;
            animator.SetBool("IsSliding", true);
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
