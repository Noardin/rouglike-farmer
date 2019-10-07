using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    private CameraShake camshake;
    private shockwaveSpawner _shockWaveSpawner;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;// How much to smooth out the movement
    [SerializeField] private float m_DropdownForce = 400f;
    [SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck; 							// A position marking where to check for ceilings
	[SerializeField] private List<Collider2D> m_CrouchDisableCollider;				// A collider that will be disabled when crouching
	[SerializeField] private Transform LedgeCheck;
	[SerializeField] private Transform WallCheck;

	public LayerMask WhatIsEdge;
	public float RangeToDropdown;
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded; // Whether or not the player is grounded.
	private bool AgainstWall;
	private bool IsLedge;
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private Vector3 DashdDircetion;
	public bool isSliding = false;
	public float DashSpeed = 400f;
	public float DashDistance = 100f;
	public float DashCurrentDistance;
	private float CurrentDashSpeed = 0;
	public Animator animator;
	private Hitboxcheck PlayerHitBox;
	public BoxCollider2D LedgeHook;
	private bool IsGripping;
	private bool WasAgainstWall;
	

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

    public UnityEvent OnDropdownLandEvent;
    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public BoolEvent OnCrouchEvent;
	
	private bool m_wasCrouching = false;
	public bool isDroppingDown = false;
	private bool AirRolled;


    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		if (OnDropdownLandEvent == null)
		{
			OnDropdownLandEvent = new UnityEvent();
		}

		
	}

    void Start()
    {
	    if (PlayerHitBox == null)
	    {
		    PlayerHitBox = GetComponentInChildren<Hitboxcheck>();
	    }
        camshake = Camera.main.GetComponent<CameraShake>();
        _shockWaveSpawner = GameObject.Find("ShockWaveSpawner").GetComponent<shockwaveSpawner>();
    }

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		
		if (m_FacingRight)
		{
			DashdDircetion = new Vector3(1, 0, 0);
		}
		else
		{
			DashdDircetion = new Vector3(-1, 0, 0);
		}
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
                if (!wasGrounded)
                {
	                animator.SetBool("Grounded", true);
	                animator.SetBool("WallSliding", false);
	                animator.SetBool("LedgeHooking", false);
	                AirRolled = false;
                    OnLandEvent.Invoke();
                }
               if (isDroppingDown)
               {
	               
	               isDroppingDown = false;

                    OnDropdownLandEvent.Invoke();

                }
            }
			
        }

		if (!m_Grounded)
		{
			animator.SetBool("Grounded", false);
		}

		

		if (isSliding)
		{
			if (CanMove())
			{
				transform.position += DashdDircetion * DashSpeed * Time.deltaTime;
				
			}
			Debug.Log("dash distance: "+DashCurrentDistance);
			DashCurrentDistance += DashSpeed * Time.deltaTime;
			if (DashCurrentDistance >= DashDistance)
			{
				isSliding = false;
				Debug.Log("doneSliding");
				animator.SetBool("IsSliding", false);
			}
		}

		
	}


	public void Move(float move, bool crouch, bool jump, bool dash)
	{
		
		//UnSliding
		if (jump || crouch)
		{	
			isSliding = false;
			animator.SetBool("IsSliding", false);
			
			
		}
		//UnGripping
		if (IsGripping && m_FacingRight && move < 0 || !m_FacingRight && move > 0 && IsGripping || crouch && IsGripping)
		{
			animator.SetBool("LedgeHooking", false);
			IsGripping = false;
			jump = true;
			LedgeHook.enabled = false;
		}
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					for (int i = 0; i < m_CrouchDisableCollider.Count; i++)
					{
						m_CrouchDisableCollider[i].enabled = false;
					}
					
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					for (int i = 0; i < m_CrouchDisableCollider.Count; i++)
					{
						m_CrouchDisableCollider[i].enabled = true;
					}

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			if (!dash && !isSliding && !IsGripping)
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
			else
			{
				if (!isSliding && !AirRolled && !IsGripping)
				{
					
					if (!m_Grounded)
					{
						AirRolled = true;
					}
					PlayerHitBox.ImInvincible(.8f);
					isSliding = true;
					Debug.Log("isSliding");


					DashCurrentDistance = 0;
					

				}
				

			}

			
			

			// If the input is moving the player right and the player is facing left...
			if (!isSliding)
			{
				if (move > 0 && !m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
			}
			
			
			//Gripping
			if (!m_Grounded && move !=0)
			{
				AgainstWall = Physics2D.Raycast(WallCheck.position, DashdDircetion, .5f, m_WhatIsGround);
				Debug.Log("againsgWall"+ AgainstWall);
				if (AgainstWall)
				{
					WasAgainstWall = true;
					animator.SetBool("WallSliding", true);
					animator.SetBool("IsJumping", false);
					IsLedge = Physics2D.Raycast(LedgeCheck.position, DashdDircetion, 1f, WhatIsEdge);
					
					if (IsLedge )
					{
						
						animator.SetBool("WallSliding", false);
						animator.SetBool("LedgeHooking", true);
						IsGripping = true;
						LedgeHook.enabled = true;
					}
				
				}else if (WasAgainstWall)
				{
					WasAgainstWall = false;
					animator.SetBool("LedgeHooking", false);
					animator.SetBool("WallSliding", false);
				}

			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			IsGripping = false;
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
		//if Player is gripping and press jump

		if (IsGripping && jump &&  (move >= 0 && m_FacingRight|| move <=0 && !m_FacingRight ))
		{
			//move to top of the ledge
			StartCoroutine(Pull());

		}
        if(crouch && !m_Grounded)
        {
	        RaycastHit2D ray = Physics2D.Raycast(m_GroundCheck.position, new Vector2(0, -1), RangeToDropdown, m_WhatIsGround);
	        Debug.Log("ray", ray.collider);
	        if (ray.collider == null)
	        {
		         PlayerHitBox.ImInvincible(.8f);
                isDroppingDown = true;
                m_Rigidbody2D.AddForce(new Vector2(0f, -m_DropdownForce));
	        }
	       
        }
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private IEnumerator Pull()
	{
		animator.SetBool("IsClimbing", true);
		IsGripping = false;
		animator.SetBool("LedgeHooking", false);
		LedgeHook.enabled = false;
		m_Rigidbody2D.gravityScale = 0;
		Vector3 pullDirection = new Vector3(0f, 1f, 0f);
		Debug.Log("pulling");
		bool MustClimbe;
		RaycastHit2D ray = Physics2D.Raycast(m_GroundCheck.position, DashdDircetion, 1f, WhatIsEdge );
		if (ray.collider == null)
		{
			MustClimbe = true;
		}
		else if(!ray.collider.gameObject.CompareTag("Edge"))
		{
			MustClimbe = true;
		}
		else
		{
			MustClimbe = false;
		}
		while(MustClimbe)
		{
			ray = Physics2D.Raycast(m_GroundCheck.position, DashdDircetion, 1f, WhatIsEdge );
			if (ray.collider != null)
			{
				if (ray.collider.gameObject.CompareTag("Edge"))
				{
					MustClimbe = false;
				}
				
			}
			
			transform.position += pullDirection  * Time.deltaTime*15f;
			yield return new WaitForSeconds(.001f);
			
		}
		
		Debug.Log("pull2");
		for (var i = 0; i < 10; i++)
		{
			transform.position += DashdDircetion * Time.deltaTime * 6f;
			yield return new WaitForSeconds(.001f);
			
		}
		
		m_Rigidbody2D.gravityScale = 3;
		yield return new WaitForSeconds(.05f);
		
		
		animator.SetBool("IsClimbing", false);
	}

	private bool CanMove()
	{
		Vector3 direction;
		if (m_FacingRight)
		{
			direction = new Vector3(1,0,0);
		}
		else
		{
			direction = new Vector3(-1,0,0);
		}

		RaycastHit2D ray = Physics2D.Raycast(m_GroundCheck.position, direction, 1f, m_WhatIsGround);
		if (ray.collider == null)
		{
			return true;
		}
		
		return false;
		
	}
}
