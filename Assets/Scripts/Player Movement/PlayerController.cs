using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] public LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] public List<Transform> m_GroundChecks;                           // Positions marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	public AK.Wwise.Event SomeSound;
	const float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
	const float wall_radius = .1f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .02f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	public Animator animator;
	private Vector3 m_Velocity = Vector3.zero;

	public float hangTime = .2f;
	private float hangCounter;

	public float jumpBufferLength = .5f;
	private float jumpBufferCount;

	public float timeBeforeNextJump = .01f;
	private float lastJumpedTimer;

	public ParticleSystem footstepsParticleSystem;
	public GameObject footstepsPosition;
	private ParticleSystem.EmissionModule footEmission;

	public SpriteRenderer spriteRenderer;

	bool isTouchingFront;
	public List<Transform> frontChecks;
	bool wallSliding;
	public float wallSlidingSpeed;

	private float move;

	public ParticleSystem impact;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private AudioManager audioManager;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		footEmission = footstepsParticleSystem.emission;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		audioManager = FindObjectOfType<AudioManager>();
	}

    private void LateUpdate()
    {
		if (move > 0)
		{
			transform.localEulerAngles = new Vector3(0, 0, -8.07f);
		}
		else if (move < 0)
		{
			transform.localEulerAngles = new Vector3(0, 0, 8.07f);
		}
		else if (move == 0)
		{
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

    private void FixedUpdate()
	{
		if (lastJumpedTimer > 0) { return; };
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		foreach(Transform m_GroundCheck in m_GroundChecks)
        {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
				{
					if (wasGrounded == false)
					{
						impact.gameObject.SetActive(true);
						impact.transform.position = footstepsPosition.transform.position;
						impact.Stop();
						impact.Play();
					}
					m_Grounded = true;
					if (!wasGrounded)
                    {
						if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Squash"))
						{
							OnLandEvent.Invoke();
							animator.SetTrigger("squash");
						}
						// transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 1.5f;
					}
					return;
				}
			}
		}
		
	}


	public void Move(float move, bool jump, bool jumpEnd)
	{
		this.move = move;
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
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
			} else if (move == 0 && !jump)
            {
				transform.localEulerAngles = new Vector3(0, 0, 0);
			}
		}

		// Wall sliding
		isTouchingFront = false;
		foreach (Transform frontCheck in frontChecks)
		{
			if (Physics2D.OverlapCircle(frontCheck.position, wall_radius, m_WhatIsGround))
            {
				isTouchingFront = true;
            }
		}
		
		if (isTouchingFront && move != 0 && !m_Grounded)
		{
			wallSliding = true;
		} else
        {
			wallSliding = false;
        }

		if (wallSliding)
        {
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Clamp(m_Rigidbody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
		}

		// Coyote time counter
		if (m_Grounded)
		{
			hangCounter = hangTime;
		}
		else
		{
			hangCounter -= Time.deltaTime;
		}

		// manager jump buffer
		if (jump)
		{
			jumpBufferCount = jumpBufferLength;
		}
		else
		{
			jumpBufferCount -= Time.deltaTime;
		}

		lastJumpedTimer -= Time.deltaTime;

		// If the player should jump...
		if (hangCounter > 0 && jumpBufferCount >= 0)
		{
			jumpBufferCount = 0;
			hangCounter = 0;
			lastJumpedTimer = timeBeforeNextJump;
			m_Grounded = false;

			impact.gameObject.SetActive(true);
			impact.transform.position = footstepsPosition.transform.position;
			impact.Stop();
			impact.Play();

			// m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
			//audioManager.Play("Jump", 0);
			SomeSound.Post(gameObject);
			animator.SetTrigger("stretch");
		}

		// Start moving faster if player lets go of space
		else if (jumpEnd && m_Rigidbody2D.velocity.y > 0)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * .5f);
		}

		//show footstep effect
		if (m_Grounded && Input.GetAxisRaw("Horizontal") != 0)
		{
			footEmission.rateOverTime = 35f;
		}
		else
		{
			footEmission.rateOverTime = 0f;
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		spriteRenderer.flipX = !spriteRenderer.flipX;
	}
}