using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float jumpForce = 5.0f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask platformLayer;

	private InputAction moveAction;
    private InputAction jumpAction;
	private Rigidbody2D rb;
	private Vector2 direction;
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
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
	}

    void Update()
    {
		if (transform.position.y <= -.5f)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
		direction = moveAction.ReadValue<Vector2>();
		if (jumpAction.WasPressedThisFrame() && isGrounded && !isJumping)
			isJumping = true;
	}

	private void FixedUpdate()
	{
		rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

		if (isJumping)
		{
			rb.AddForceY(jumpForce, ForceMode2D.Impulse);
			isJumping = false;
		}
	}
}
