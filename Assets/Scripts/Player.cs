using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
	[SerializeField] private Menu menu;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float jumpForce = 5.0f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask platformLayer;

	private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction reloadAction;
	private Rigidbody2D rb;
	private Animator animator;
	private Vector2 direction;
	private SpriteRenderer sprite;
	private bool isJumping = false;
	private bool isGrounded;

	public int keys = 0;

	private void OnEnable()
	{
		inputActions.FindActionMap("Player").Enable();
	}

	private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
		reloadAction = InputSystem.actions.FindAction("Reload");
	}

    void Update()
    {
		if (!menu.isPaused)
		{
			if (transform.position.y <= -.5f || reloadAction.WasPressedThisFrame())
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);

			isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
			animator.SetBool("isGrounded", isGrounded);

			direction = moveAction.ReadValue<Vector2>();
			if (direction.x < 0)
				sprite.flipX = true;
			else if (direction.x > 0)
				sprite.flipX = false;

			if (jumpAction.WasPressedThisFrame() && isGrounded && !isJumping)
			{
				isJumping = true;
				animator.SetBool("isJumping", isJumping);
			}
		}
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
		animator.SetFloat("speed", Mathf.Abs(direction.x));
		animator.SetFloat("velocity", rb.linearVelocityY);

		if (isJumping)
		{
			rb.AddForceY(jumpForce, ForceMode2D.Impulse);
			isJumping = false;
			animator.SetBool("isJumping", isJumping);
		}
	}
}
